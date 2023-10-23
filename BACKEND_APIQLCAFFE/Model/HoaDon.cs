namespace BACKEND_APIQLCAFFE.Model
{
    public class HoaDon
    {
        public int Id_HoaDon { get; set; }
        public int Id_ThucDon { get; set; }
        public int Id_Ban {  get; set; }
        public int SoLuong { get; set; }
        public float ThanhTien { get; set; }
        public int Id_User {  get; set; }
        public string CreateAt { get; set; }
        public string UpdateAt { get; set; }
        public string Name { get; set; }
    }
}
