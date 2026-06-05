using Microsoft.AspNetCore.Mvc;
using ModelBinding_FromForm.Models;

namespace ModelBinding_FromForm.Controllers
{
    // ====================================================================
    // REGION 00: CONTROLLER OVERVIEW — [FromForm] என்றால் என்ன?
    // ====================================================================
    //
    //  [FromForm] — HTTP multipart/form-data request-இல் வரும் data-ஐ
    //               controller action parameter-ஆக bind பண்ண use ஆகும்.
    //
    //  எப்போது [FromForm] use பண்ணணும்?
    //  ─────────────────────────────────
    //  ✅ File upload (IFormFile) இருக்கும்போது — MUST use [FromForm]
    //  ✅ HTML <form method="POST"> submit பண்ணும்போது
    //  ✅ Postman-ல "form-data" option select பண்ணும்போது
    //  ✅ Text fields + File upload சேர்த்து ஒரே request-ல அனுப்பும்போது
    //
    //  எப்போது [FromForm] வேண்டாம்?
    //  ──────────────────────────────
    //  ❌ JSON body அனுப்பும்போது  → [FromBody] use பண்ணணும்
    //  ❌ URL query string-ல data வரும்போது → [FromQuery] use பண்ணணும்
    //  ❌ GET request-ல data படிக்கும்போது → GET-ல [FromForm] வேலை செய்யாது!
    //
    //  POST vs GET — [FromForm]-க்கு ஏன் POST மட்டுமே?
    //  ──────────────────────────────────────────────────
    //  GET  → Data URL-ல போகும் (visible, limited size, no file support)
    //  POST → Data request body-ல போகும் (hidden, large data, file support ✅)
    //  [FromForm] எப்பவும் POST/PUT/PATCH-உடன் மட்டுமே use பண்ணணும்.
    //
    // ====================================================================

    [ApiController]
    [Route("api/[controller]")]
    public class Employee : ControllerBase
    {

        // ════════════════════════════════════════════════════════════════
        // METHOD 1: CreateEmployeeProfile — File Upload with Profile Data
        // ════════════════════════════════════════════════════════════════
        //
        //  Route  : POST api/employee/create-profile
        //  Purpose: Employee name, designation, department + profile image
        //           சேர்த்து ஒரே request-ல server-கு அனுப்புவோம்.
        //
        //  [FromForm] இங்கே ஏன் தேவை?
        //  → EmployeeCreateRequestDto-ல IFormFile (ProfileImage) இருக்கு.
        //  → File + Text data சேர்த்து bind பண்ண [FromForm] மட்டுமே capable.
        //  → [FromBody] use பண்ணினா file bind ஆகாது — runtime error வரும்.
        //
        // ════════════════════════════════════════════════════════════════

        #region --- 01. Method 1: Create Employee Profile with Image Upload ---

        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateEmployeeProfile([FromForm] EmployeeCreateRequestDto request)
        {

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 1A: Null Check — Request வந்துச்சா இல்லையா?    │
            // └─────────────────────────────────────────────────────────┘
            //
            //  Front-end form submit பண்ணாம directly API hit பண்ணினா
            //  அல்லது body empty-ஆ அனுப்பினா → request null ஆகும்.
            //  அதை check பண்ணி 400 Bad Request return பண்றோம்.

            #region Region 1A: Null Check

            if (request == null)
            {
                return BadRequest(new { error = "Request data empty-ah iruku machan!" });
            }

            #endregion

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 1B: File Presence Check — Image select பண்ணாரா? │
            // └─────────────────────────────────────────────────────────┘
            //
            //  User form-ல file select பண்ணாம submit பண்ணினா,
            //  IFormFile property null ஆகும் அல்லது Length = 0 ஆகும்.
            //  இந்த validation இல்லாம போனா — empty file save attempt
            //  பண்ணி server-ல corrupt file create ஆகும்.

            #region Region 1B: File Presence Validation

            if (request.ProfileImage == null || request.ProfileImage.Length == 0)
            {
                return BadRequest(new { error = "Profile image rumba mukkiyam! Adha upload pannu mavee." });
            }

            #endregion

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 1C: File Type Check — Allowed formats மட்டும்   │
            // └─────────────────────────────────────────────────────────┘
            //
            //  Security reason-ஆல .exe, .php, .js போன்ற dangerous files
            //  upload ஆகாம தடுக்கணும்.
            //  Path.GetExtension() → file name-இல் இருந்து extension மட்டும் எடுக்கும்.
            //  Example: "myphoto.PNG" → ".png" (ToLower() பண்றோம்)

            #region Region 1C: File Extension Validation

            var fileExtension = Path.GetExtension(request.ProfileImage.FileName).ToLower();
            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
            {
                return BadRequest(new { error = "Dei! JPG, JPEG illa PNG format files mattum thaan allow panna mudiyum." });
            }

            #endregion

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 2: File Storage — Server-ல Image Save பண்றோம்  │
            // └─────────────────────────────────────────────────────────┘
            //
            //  Step 1: Project root → "Uploads/Profiles" folder path set பண்றோம்
            //  Step 2: Folder இல்லாட்டி auto-create பண்றோம்
            //  Step 3: Guid.NewGuid() use பண்ணி unique file name உருவாக்கறோம்
            //          (same name conflict / overwrite தவிர்க்க)
            //  Step 4: FileStream + CopyToAsync → async-ஆ disk-ல save பண்றோம்
            //          (async use பண்றதால் thread block ஆகாது — performance better)

            #region Region 2: File Storage System

            string uniqueFileName = string.Empty;
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Profiles");

            try
            {
                // Folder இல்லாட்டி create பண்று
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Unique file name: "a1b2c3d4-e5f6_myphoto.jpg" format
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.ProfileImage.FileName;

                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                // FileStream: physical file create பண்ணி binary data write பண்றோம்
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfileImage.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                // Permission denied, disk full போன்ற server-side errors handle பண்றோம்
                return StatusCode(500, new { error = $"Server folder-la file save panna mudiyala: {ex.Message}" });
            }

            #endregion

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 3: DB Mapping — Image path DB-ல store பண்றோம்  │
            // └─────────────────────────────────────────────────────────┘
            //
            //  Industry Best Practice:
            //  Image binary data-ஐ DB-ல நேரடியா store பண்ணக்கூடாது.
            //  ஏன்? → DB size explode ஆகும், query slow ஆகும்.
            //  Instead → file server-ல save பண்ணி, DB-ல relative URL மட்டும் store பண்ணு.
            //  Example stored value: "/Uploads/Profiles/abc123_photo.jpg"
            //  Front-end: <img src="/Uploads/Profiles/abc123_photo.jpg" /> — இப்படி use பண்ணலாம்.

            #region Region 3: Database Mapping (Image URL store)

            string dbImageUrlPath = $"/Uploads/Profiles/{uniqueFileName}";

            /*
             * Entity Framework context ஒரு real project-ல இப்படி இருக்கும்:
             *
             * var newEmployee = new Employee {
             *     Name        = request.EmployeeName,
             *     Designation = request.Designation,
             *     Department  = request.Department,
             *     ImageUrl    = dbImageUrlPath   // Full binary இல்ல, URL மட்டும்!
             * };
             * _context.Employees.Add(newEmployee);
             * await _context.SaveChangesAsync();
             */

            #endregion

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 4: Success Response — Front-end-கு JSON return  │
            // └─────────────────────────────────────────────────────────┘
            //
            //  200 OK + anonymous object → JSON serialized ஆகி front-end-கு போகும்.
            //  savedPath → front-end இந்த URL use பண்ணி image render பண்ணலாம்.

            #region Region 4: API Response

            return Ok(new
            {
                status = "Success",
                message = "Employee Profile with Image successfully uploaded and created!",
                data = new
                {
                    name = request.EmployeeName,
                    role = request.Designation,
                    dept = request.Department,
                    savedPath = dbImageUrlPath
                }
            });

            #endregion
        }

        #endregion


        // ════════════════════════════════════════════════════════════════
        // METHOD 2: CreateUser — Simple Form Data (No File Upload)
        // ════════════════════════════════════════════════════════════════
        //
        //  Route  : POST api/employee  (Route attribute இல்லை — controller route மட்டும்)
        //  Purpose: Basic user data (name, email etc.) form-ல இருந்து படிக்கணும்.
        //
        //  [FromForm] இங்கே ஏன்?
        //  → HTML form submit அல்லது Postman form-data-ல வரும் data bind பண்ண.
        //  → File இல்லாட்டாலும் form submit data-ஐ [FromForm] மட்டுமே correctly bind பண்ணும்.
        //  → [FromBody] பண்ணினா Content-Type: application/json expect பண்ணும் — mismatch வரும்.
        //
        // ════════════════════════════════════════════════════════════════

        #region --- 02. Method 2: Create User (Simple Form Binding) ---

        [HttpPost]
        public IActionResult CreateUser([FromForm] UserModel user)
        {
            // ┌─────────────────────────────────────────────────────────┐
            // │  Form-ல வந்த user data-ஐ response-ஆ return பண்றோம்    │
            // │  Real project-ல இங்கே DB save logic வரும்              │
            // └─────────────────────────────────────────────────────────┘

            #region Region 1: Build and Return Response

            var response = new
            {
                Success = true,
                Message = $"User {user.Name} created successfully!",
                Code = StatusCodes.Status200OK
            };
            return Ok(response);

            #endregion
        }

        #endregion


        // ════════════════════════════════════════════════════════════════
        // METHOD 3: AddUser — File Upload with Custom Route [action]
        // ════════════════════════════════════════════════════════════════
        //
        //  Route  : POST api/employee/adduser  ([action] → method name தானே route ஆகும்)
        //  Purpose: User data + profile picture upload பண்ணி server-ல save பண்ணணும்.
        //
        //  Method 1 vs Method 3 வித்தியாசம்:
        //  Method 1 → GUID prefix + subfolder "Profiles/" → production-ready approach
        //  Method 3 → Original filename மட்டும், root "Uploads/" folder → simpler approach
        //             (Same name file மீண்டும் upload பண்ணா overwrite ஆகும் — risk இருக்கு)
        //
        //  [action] route token என்னன்னா?
        //  → [Route("[action]")] = method name-ஐ route-ஆ use பண்ணும்.
        //  → AddUser method → route: api/employee/adduser
        //
        // ════════════════════════════════════════════════════════════════

        #region --- 03. Method 3: Add User with Profile Picture Upload ---

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddUser([FromForm] UserModel user)
        {

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 1: File Null Check — Image வந்துச்சா பார்க்கோம் │
            // └─────────────────────────────────────────────────────────┘
            //
            //  ProfilePicture null-ஆ இருந்தா file upload பண்ணலை என்று அர்த்தம்.
            //  Null check இல்லாம போனா NullReferenceException வரும்.

            #region Region 1: File Null Check and Upload

            if (user.ProfilePicture != null)
            {
                // ┌───────────────────────────────────────────────────────┐
                // │  REGION 2: Folder Create — "Uploads" folder ready பண்  │
                // └───────────────────────────────────────────────────────┘
                //
                //  Method 1 போலவே — folder இல்லாட்டி auto-create பண்றோம்.
                //  இங்கே "Profiles" subfolder இல்லை — root "Uploads" மட்டும்.

                #region Region 2: Ensure Upload Folder Exists

                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                #endregion

                // ┌───────────────────────────────────────────────────────┐
                // │  REGION 3: File Save — Original filename-ஓட save     │
                // └───────────────────────────────────────────────────────┘
                //
                //  ⚠️ Note: இங்கே GUID prefix இல்லை.
                //  Same name file upload பண்ணா → previous file overwrite ஆகும்.
                //  Production-ல Method 1-ல உள்ள GUID approach follow பண்ணணும்.

                #region Region 3: Save File to Disk

                var filePath = Path.Combine(uploadsFolderPath, user.ProfilePicture.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await user.ProfilePicture.CopyToAsync(stream);
                }

                #endregion

                // ┌───────────────────────────────────────────────────────┐
                // │  REGION 4: Success Response — File info return பண்றோம் │
                // └───────────────────────────────────────────────────────┘

                #region Region 4: Return Success Response

                var response = new
                {
                    Success = true,
                    Message = $"User {user.Name} created successfully!",
                    ProfilePictureName = user?.ProfilePicture?.FileName,
                    Code = StatusCodes.Status200OK
                };
                return Ok(response);

                #endregion
            }

            #endregion

            // ┌─────────────────────────────────────────────────────────┐
            // │  REGION 5: Validation Failure — File இல்லாட்டி 400     │
            // └─────────────────────────────────────────────────────────┘
            //
            //  ProfilePicture null-ஆ இருந்தா ModelState errors return பண்றோம்.
            //  ModelState → Data Annotations ([Required], [MaxLength] etc.) errors collect பண்ணும்.

            #region Region 5: BadRequest if No File

            return BadRequest(ModelState);

            #endregion
        }

        #endregion

    }
}