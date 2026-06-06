using Microsoft.AspNetCore.Http;

namespace ModelBinding_FromForm.Controllers
{
    // Indha data DTO class thaan `[FromForm]` valiya model-bind aagi values capture pannuudhu
    public class EmployeeCreateRequestDto
    {
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }

        // IFormFile thaan HTTP 'multipart/form-data' payload la vara binary image-ah capture pannum
        public IFormFile ProfileImage { get; set; }
    }
}