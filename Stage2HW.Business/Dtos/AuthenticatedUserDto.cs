namespace Stage2HW.Business.Dtos
{
    public class AuthenticatedUserDto
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
