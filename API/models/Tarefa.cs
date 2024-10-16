namespace API.models;

public class Tarefa
{
    public int TarefaId { get; set; }
    public String? Nome { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
