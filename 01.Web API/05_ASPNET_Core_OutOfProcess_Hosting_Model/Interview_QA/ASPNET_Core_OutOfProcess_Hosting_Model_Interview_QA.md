# ASP.NET Core Out Of Process Hosting Model - Interview Q&A

## Beginner Questions

### 1. What is ASP.NET Core Out Of Process Hosting Model?

It means the ASP.NET Core application runs in a separate process from IIS.

Real-time usage:
IIS can work as a front server while Kestrel runs the ASP.NET Core application behind it.

Common mistake:
Thinking out-of-process means the app runs outside the machine. It only means a separate process.

### 2. What is Kestrel in ASP.NET Core?

Kestrel is the web server used by ASP.NET Core.

Real-time usage:
It often runs the actual ASP.NET Core application, especially in reverse proxy style setups.

Common mistake:
Confusing Kestrel with IIS. They can work together.

### 3. What role does IIS play in out-of-process hosting?

IIS receives the request first and forwards it to the ASP.NET Core application.

Common mistake:
Assuming IIS is doing all ASP.NET Core request processing by itself.

## Intermediate Questions

### 4. What is the main difference between in-process and out-of-process hosting?

In-process:

- app runs inside IIS worker process

Out-of-process:

- app runs in a separate process, usually behind Kestrel

### 5. Does out-of-process hosting change the controller-service architecture?

No.

Real-time usage:
Controllers, services, models, and DI usually remain the same.

### 6. Why is it useful to know about reverse proxy style hosting?

Because many real applications are deployed behind a front server such as IIS, Nginx, or Apache.

Common mistake:
Focusing only on code and ignoring hosting flow.

## Advanced Questions

### 7. Why can out-of-process hosting have more overhead than in-process hosting?

Because the request is forwarded from IIS to another separate process.

Real-time usage:
This can matter in performance discussions for high-traffic APIs.

### 8. Does `Program.cs` tell IIS to use out-of-process hosting?

No.

`Program.cs` configures the application pipeline, but hosting mode is mainly decided by hosting setup and deployment configuration.

Common mistake:
Trying to find one line in code that "enables" out-of-process mode.

### 9. Why should backend developers learn hosting models?

Because real-world backend work includes:

- deployment
- performance
- diagnostics
- production troubleshooting

## Real-Time Scenario Questions

### 10. Your API works locally but behaves differently behind IIS. What should you check?

Check:

- hosting model
- IIS configuration
- runtime installation
- environment name
- forwarded request behavior

### 11. A teammate says Kestrel is not needed when IIS exists. Is that always correct?

No.

In out-of-process hosting, Kestrel usually runs the ASP.NET Core application and IIS forwards requests to it.

### 12. Why is `GET /api/hosting/current` useful in this lesson?

It helps us see runtime details in a practical and simple way.

Common mistake:
Trying to learn hosting only from theory without seeing runtime behavior.

## Quick Revision Points

- out-of-process means separate process from IIS
- Kestrel usually runs the ASP.NET Core app
- IIS acts as front server
- request is forwarded to ASP.NET Core process
- business architecture remains the same
