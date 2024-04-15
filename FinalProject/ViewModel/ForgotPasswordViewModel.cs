using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel;

public class ForgotPasswordViewModel
{
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
