using System.ComponentModel.DataAnnotations;

namespace ClinicSoftware.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Informe o nome do cliente")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string ClientName { get; set; }

        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 caracteres")]
        public string ClientCPF { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClientBirthday { get; set; }

        [Required(ErrorMessage = "Informe o telefone para contato")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string ClientPhoneNumber { get; set; }

        [Display(Name = "Telefone para recado")]
        [DataType(DataType.PhoneNumber)]
        public string ClientAlternateContact { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string ClientEmail { get; set; }

        [Display(Name = "Imagem de perfil")]
        [StringLength(200)]
        public string ProfileImageUrl { get; set; }
    }
}
