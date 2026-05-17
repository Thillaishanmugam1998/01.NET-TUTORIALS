# ASP.NET Core Kestrel Web Server - Interview Q&A

## Beginner Questions

### 1. What is Kestrel in ASP.NET Core?

Kestrel is the default web server used by ASP.NET Core applications.

Real-time usage:
When developers run an ASP.NET Core API locally, Kestrel usually handles the request.

Common mistake:
Thinking Kestrel is part of controller code. It is the web server.

### 2. What does a web server do in a Web API application?

A web server listens for incoming requests and passes them to the application.

Real-time usage:
Kestrel receives client requests and hands them to the ASP.NET Core pipeline.

### 3. Is Kestrel only for development?

No.

Kestrel is used in both development and production.

Common mistake:
Thinking Kestrel is only a local testing server.

## Intermediate Questions

### 4. Is Kestrel cross-platform?

Yes.

Kestrel works on:

- Windows
- Linux
- macOS

### 5. Can Kestrel run behind IIS?

Yes.

Real-time usage:
In many deployments, IIS works in front and Kestrel runs the ASP.NET Core application.

### 6. Does Kestrel replace controllers or services?

No.

Kestrel is the web server, while controllers and services are application code layers.

## Advanced Questions

### 7. Why is Kestrel important in ASP.NET Core architecture?

Because it is the server that actually hosts and runs the ASP.NET Core application.

Real-time usage:
Understanding Kestrel helps with deployment, performance, and troubleshooting.

### 8. Can Kestrel run directly without IIS, Nginx, or Apache?

Yes.

ASP.NET Core apps can run directly on Kestrel.

Common mistake:
Assuming IIS is always required.

### 9. Why should backend developers understand Kestrel?

Because real backend work includes:

- application hosting
- environment setup
- diagnostics
- deployment troubleshooting

## Real-Time Scenario Questions

### 10. Your API starts with `dotnet run`. Which server is usually serving requests?

Kestrel usually serves the requests.

### 11. A teammate says Kestrel is only useful when IIS is not present. Is that fully correct?

No.

Kestrel can work directly, and it can also work behind IIS or other reverse proxies.

### 12. Why is `GET /api/server/current` useful in this lesson?

It helps us connect theory with runtime behavior by showing simple server details.

Common mistake:
Trying to learn server concepts without checking runtime behavior.

## Quick Revision Points

- Kestrel is the default ASP.NET Core web server
- it listens for HTTP and HTTPS requests
- it is cross-platform
- it can run directly or behind reverse proxies
- controllers and services remain separate application layers
