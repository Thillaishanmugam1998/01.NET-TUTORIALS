# ASP.NET Core In-Process Hosting Model - Interview Q&A

## Beginner Questions

### 1. What is ASP.NET Core In-Process Hosting Model?

It means the ASP.NET Core application runs inside the IIS worker process.

Real-time usage:
Companies using Windows Server and IIS often use this model for ASP.NET Core APIs.

Common mistake:
Thinking the app always runs in-process in every hosting environment. It is mainly an IIS hosting concept.

### 2. What is the IIS worker process?

It is the IIS process that handles web requests.

Common examples:

- `w3wp.exe`
- `iisexpress.exe`

Common mistake:
Not understanding that IIS itself has its own process for running web applications.

### 3. Why do we use in-process hosting?

Because it improves performance and reduces request overhead.

Real-time usage:
Useful for enterprise APIs hosted on Windows with IIS.

## Intermediate Questions

### 4. What is the difference between in-process and out-of-process hosting?

In in-process hosting:

- app runs inside IIS worker process

In out-of-process hosting:

- IIS forwards request to a separate ASP.NET Core process, usually through Kestrel

Common mistake:
Thinking both models behave exactly the same internally.

### 5. Does in-process hosting change controller or service code?

No. It mainly changes how the app is hosted.

Real-time usage:
Business code usually remains the same.

### 6. Is in-process hosting mainly for Windows or Linux?

It is mainly for Windows IIS hosting.

Common mistake:
Assuming this is a general hosting model for all platforms.

## Advanced Questions

### 7. Why is in-process hosting generally faster than out-of-process hosting on IIS?

Because request handling stays inside the same process and avoids extra forwarding cost.

Real-time usage:
This matters in high-traffic enterprise APIs.

### 8. Does `Program.cs` directly enable in-process hosting?

No. `Program.cs` builds and configures the app, but hosting mode is mainly controlled by IIS hosting setup and deployment configuration.

Common mistake:
Trying to find one code line in `Program.cs` that "turns on" in-process mode.

### 9. How can you identify the current process at runtime?

You can inspect:

- process id
- process name
- environment details

In this lesson we exposed them through `GET /api/hosting/current`.

## Real-Time Scenario Questions

### 10. Your API is hosted on IIS and performance is poor. Would hosting model be one thing to review?

Yes.

You should review:

- hosting model
- application pool settings
- logging overhead
- server resources

Common mistake:
Blaming only controller code without checking infrastructure.

### 11. A developer sees process name `dotnet` locally and expects `w3wp`. Is that always wrong?

No.

If the app is run directly using a project profile or `dotnet run`, process name can differ from IIS hosting.

Real-time usage:
Always check the actual environment before drawing conclusions.

### 12. Why is it useful to know hosting model as a backend developer?

Because backend work is not only code.

You must also understand:

- deployment
- performance
- diagnostics
- production behavior

This is a strong real-time skill.

## Quick Revision Points

- in-process hosting means app runs inside IIS worker process
- commonly used with IIS on Windows
- better performance than out-of-process in many IIS scenarios
- does not change core controller-service architecture
- local process name may differ based on launch profile
