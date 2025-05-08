namespace Docker_Update_Center.Models
{
    public class ContainerInfo
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public string? Ports { get; set; }
        public string? Created { get; set; }
    }
}
