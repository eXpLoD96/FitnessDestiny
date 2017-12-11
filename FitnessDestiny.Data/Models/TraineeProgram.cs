namespace FitnessDestiny.Data.Models
{
    public class TraineeProgram
    {
        public string TraineeId { get; set; }

        public User Trainee { get; set; }

        public int ProgramId { get; set; }

        public Program Program { get; set; }
    }
}
