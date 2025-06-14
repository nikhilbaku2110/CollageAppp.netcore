//using CollageAppp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollageAppp.Models;

public class StudentDTO
{
    [ValidateNever]
    public int Id { get; set; }

    [Required(ErrorMessage = "student name is required3.")]
    [StringLength(30)]
    public string StudentName { get; set; }

    [EmailAddress(ErrorMessage = "plz enter the valid email address")]
    public string Email { get; set; }
    
    [Required]
    public string Address { get; set; }
    public DateTime DOB { get; set; }





}
