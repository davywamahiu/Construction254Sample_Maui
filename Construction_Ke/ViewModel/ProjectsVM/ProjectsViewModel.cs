using Construction_Ke.Views.ProjectsDS;

namespace Construction_Ke.ViewModel.ProjectsVM
{
    public class ProjectsViewModel
    {
        public ProjectsViewModel()
        {
            LoadAddProjects = new(GetLoadAddProjects);
            LoadAllProjects = new(GetLoadAllProjects);
        }
        public Command LoadAllProjects { get; }
        public Command LoadAddProjects { get; }
        private async void GetLoadAllProjects(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ProjectList));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadAddProjects(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddProjectsToList));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
    }
}
