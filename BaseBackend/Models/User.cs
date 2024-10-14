public class User
{
    public int UserID { get; set; }               // Unique identifier for the user
    public string Username { get; set; }          // User's username
    public string PasswordHash { get; set; }      // Hashed password (should not store plain text passwords)
    public string Email { get; set; }             // User's email address
    public string FullName { get; set; }          // Full name of the user
    public string Address { get; set; }           // User's address (street and number)
    public string City { get; set; }              // City where the user resides
    public string State { get; set; }             // State or province
    public string Role { get; set; }             // State or province

    public string ZipCode { get; set; }           // Postal code
    public string Country { get; set; }           // Country
    public string PhoneNumber { get; set; }       // User's phone number
    public DateTime DateOfBirth { get; set; }     // Date of birth
    public string Credentials { get; set; }       // Any credentials (e.g., job titles, certifications)
    public DateTime CreatedAt { get; set; }       // Account creation date
    public DateTime? LastLoginAt { get; set; }    // Last login date (nullable if user hasn't logged in)
    public bool IsActive { get; set; }            // Active/inactive status
}
