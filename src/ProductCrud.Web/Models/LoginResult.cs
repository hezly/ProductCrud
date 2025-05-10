﻿namespace ProductCrud.Web.Models;

public class LoginResult
{
    public bool Succeeded { get; set; }
    public string? TokenType { get; set; }
    public string? AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
    public string? Error { get; set; }
}
