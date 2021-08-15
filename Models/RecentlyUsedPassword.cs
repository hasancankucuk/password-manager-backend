namespace password_manager_backend.Models
{
    public class RecentlyUsedPassword
    {
        public int Id { get; set; }
        public int UserInfoModelId { get; set; }
        virtual public UserInfoModel UserInfoModel { get; set; }

        public int SaveAccountInfoModelId { get; set; }
        public virtual SaveAccountInfoModel SaveAccountInfoModel {get;set;}
}
}
