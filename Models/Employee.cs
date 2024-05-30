using System.ComponentModel.DataAnnotations;

namespace ClinicSoftware.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Informe o nome do fucionário")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string? EmployeeName { get; set; }

        [Display(Name = "CPF")]
        [StringLength(11, ErrorMessage = "O CPF deve conter 11 caracteres")]
        public string? EmployeeCpf { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeBirthday { get; set; }

        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string? EmployeePhoneNumber { get; set; }

        [Display(Name = "Telefone para recado")]
        [DataType(DataType.PhoneNumber)]
        public string? EmployeeAlternateContact { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string? EmployeeEmail { get; set; }

    }
}
