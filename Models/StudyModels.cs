namespace StudyTrackerAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    
    // --- NOVAS COLUNAS PARA A SEQUÊNCIA (STREAK) ---
    public int Streak { get; set; } = 0;
    public string LastStudyDate { get; set; } = string.Empty;
}

public class StudyTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Due { get; set; } = string.Empty;
    public string Priority { get; set; } = "media";
    public bool Done { get; set; }
}

public class StudyEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string Type { get; set; } = "Aula";
}

public class Subject
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Professor { get; set; } = string.Empty;
    public int Progresso { get; set; }
}

public class Grade
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public double Value { get; set; }
    public double Weight { get; set; }
}

public class StudySession
{
    public int Id { get; set; }
    public string Date { get; set; } = string.Empty;
    public int Minutes { get; set; }
    
    // --- NOVAS COLUNAS PARA O ESTUDO ---
    public string Subject { get; set; } = string.Empty; // O que o aluno estudou (ex: Matemática)
    public int UserId { get; set; } // De quem é este estudo? (Relacionamento com a tabela User)
}

public class Goal
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; } = false;
}