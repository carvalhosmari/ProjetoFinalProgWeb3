using System.ComponentModel.DataAnnotations;

namespace ProgWeb3APIEventos.Core.Model
{
    public class CityEvent
    {
        public long IdEvent { get; set; }
        [Required(ErrorMessage = "O evento precisa ter um título.")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "A data não pode ser vazia.")]
        public DateTime DateHourEvent { get; set; }
        [Required(ErrorMessage = "É preciso informar o local do evento.")]
        public string Local { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}