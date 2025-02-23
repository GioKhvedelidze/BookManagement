﻿using Microsoft.AspNetCore.Identity;

namespace BookManagement.Models.Models;

public class User 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User";
}