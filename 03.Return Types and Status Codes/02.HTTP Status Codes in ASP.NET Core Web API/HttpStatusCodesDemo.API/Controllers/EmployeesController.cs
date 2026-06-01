#region HTTP Status Codes Overview

/*
==================================================
HTTP Status Code என்றால் என்ன?
==================================================

Client ஒரு Request அனுப்பும்போது,
Server அந்த Request-ன் Result-ஐ 3 digit number மூலம் தெரிவிக்கும்.
அதுதான் HTTP Status Code.

Series:

1xx - Informational
2xx - Success
3xx - Redirection
4xx - Client Error
5xx - Server Error

==================================================
1xx - Informational Responses
==================================================

Example:
100 Continue

Common Codes:
100 Continue
101 Switching Protocols
102 Processing
103 Early Hints

==================================================
2xx - Success Responses
==================================================

Example:
200 OK

Common Codes:
200 OK
201 Created
202 Accepted
204 No Content
206 Partial Content

Sample Method:

[HttpGet("success")]
public IActionResult Success()
{
    return Ok("Request completed successfully.");
}

==================================================
3xx - Redirection Responses
==================================================

Example:
301 Moved Permanently

Common Codes:
300 Multiple Choices
301 Moved Permanently
302 Found
303 See Other
304 Not Modified
307 Temporary Redirect
308 Permanent Redirect

Sample Method:

[HttpGet("redirect")]
public IActionResult RedirectDemo()
{
    return Redirect("https://example.com");
}

==================================================
4xx - Client Error Responses
==================================================

Example:
404 Not Found

Common Codes:
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
405 Method Not Allowed
408 Request Timeout
409 Conflict
415 Unsupported Media Type
429 Too Many Requests

Sample Method:

[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    if (id <= 0)
    {
        return BadRequest("Invalid Id.");
    }

    return NotFound();
}

==================================================
5xx - Server Error Responses
==================================================

Example:
500 Internal Server Error

Common Codes:
500 Internal Server Error
501 Not Implemented
502 Bad Gateway
503 Service Unavailable
504 Gateway Timeout

Sample Method:

[HttpGet("server-error")]
public IActionResult ServerError()
{
    return StatusCode(
        StatusCodes.Status500InternalServerError,
        "Unexpected server error."
    );
}

==================================================

*/

#endregion

