using System;
using System.ComponentModel.DataAnnotations;
using MyMvcApp.Models;

public class Student
{
    public int Id { get; set; } // Mã sinh viên

    [Required(ErrorMessage = "Họ tên bắt buộc phải nhập")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Tên phải từ 4 đến 100 ký tự")]
    [Display(Name = "Họ và tên")]
    public string Name { get; set; } // Họ tên

    [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có đuôi gmail.com")]
    [Display(Name = "Địa chỉ Email")]
    public string Email { get; set; } // Email

    [Required(ErrorMessage = "Mật khẩu bắt buộc phải nhập")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 đến 100 ký tự")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ hoa, 1 chữ thường, 1 chữ số và 1 ký tự đặc biệt")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } // Mật khẩu

    [Required(ErrorMessage = "Vui lòng chọn ngành học")]
    [Display(Name = "Ngành học")]
    public Branch? Branch { get; set; } // Ngành học

    [Required(ErrorMessage = "Vui lòng chọn giới tính")]
    [Display(Name = "Giới tính")]
    public Gender? Gender { get; set; } // Giới tính

    [Display(Name = "Hệ đào tạo")]
    public bool IsRegular { get; set; } // Hệ: true = chính quy, false = phi chính quy

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } // Địa chỉ

    [Required(ErrorMessage = "Ngày sinh bắt buộc nhập")]
    [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Ngày sinh phải trong khoảng 1963 - 2005")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")]
    public DateTime DateOfBirth { get; set; } // Ngày sinh

    [Required(ErrorMessage = "Điểm bắt buộc nhập")]
    [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0.0 đến 10.0")]
    [Display(Name = "Điểm")]
    public double Score { get; set; } // Điểm

    public string? Avatar { get; set; }
}
