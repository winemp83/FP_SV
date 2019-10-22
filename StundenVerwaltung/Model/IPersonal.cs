namespace StundenVerwaltung.Model
{
    public interface IPersonal
    {
        string ID { get; set; }
        bool IstUnterrichtet { get; set; }
        string MID { get; set; }
        string Name { get; set; }
        double Stunden { get; set; }
        string VName { get; set; }
    }
}