namespace Hospital.API.Data.EmailServices;

public static class EmailTemplateService
{
    public static string CreateEmailBody()
    {
        return @"
        <html>
        <body style='font-family: Arial, sans-serif;'>
            <h2 style='color: #2c5aa0;'>🐾 New Pet Admission Alert</h2>
            
            <div style='background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                <h3>Pet Information:</h3>
                <ul style='list-style-type: none; padding: 0;'>
                    <li><strong>Name:</strong> {0}</li>
                    <li><strong>Species:</strong> {1}</li>
                    <li><strong>Transfer Reason:</strong> {2}</li>
                    <li><strong>Admission Time:</strong> {3:yyyy-MM-dd HH:mm} UTC</li>
                </ul>
            </div>
            
            {4}
            
            <!-- Treatment Button - Personalized for each doctor -->
            <div style='text-align: center; margin: 30px 0;'>
                <table cellpadding='0' cellspacing='0' style='margin: 0 auto;'>
                    <tr>
                        <td style='background-color: #28a745; border-radius: 6px;'>
                            <a href='http://localhost:5028/api/medicalrecords/start?petId={5}&doctorEmail={6}' 
                               style='color: #ffffff; text-decoration: none; padding: 15px 30px; display: block; font-weight: bold; font-size: 16px;'>
                                🏥 Start Treatment
                            </a>
                        </td>
                    </tr>
                </table>
            </div>
            
            <p style='text-align: center; color: #6c757d; font-size: 14px; margin-top: 10px;'>
                Click the button above to begin treatment for this pet
            </p>
            
            <p style='color: #6c757d; font-size: 14px;'>
                Please check the hospital system for complete medical record details.
            </p>
            
            <hr style='margin: 30px 0;'>
            <p style='color: #6c757d; font-size: 12px;'>
                This is an automated notification from PHA Hospital System
            </p>
        </body>
        </html>";
    }
}