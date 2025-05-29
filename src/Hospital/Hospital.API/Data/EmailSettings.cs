﻿namespace Hospital.API.Data;

public class EmailSettings
{
    public string SmtpServer { get; set; } = string.Empty;
    
    public int Port { get; set; }
    
    public string SenderEmail { get; set; } = string.Empty;
    
    public string SenderPassword { get; set; } = string.Empty;
    
    public string SenderName { get; set; } = string.Empty;
}