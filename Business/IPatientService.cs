namespace ClinicApp.Business
{
    public interface IPatientService
    {
        void CreatePatient();
        void ViewPatients();
        void SearchByDni();
    }
}