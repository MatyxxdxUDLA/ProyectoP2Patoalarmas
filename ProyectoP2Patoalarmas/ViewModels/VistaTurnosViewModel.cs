using ProyectoP2Patoalarmas.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoP2Patoalarmas.ViewModels
{
    public class VistaTurnosViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        public ObservableCollection<Turno> Turnos { get; set; }

        public VistaTurnosViewModel()
        {
            _context = new AppDbContext();
            Turnos = new ObservableCollection<Turno>();
            CargarTurnos();
        }

        private async Task CargarTurnos()
        {
            var turnos = await _context.Turnos.Include(t => t.Usuario).Include(t => t.Servicio).ToListAsync();
            Turnos.Clear();
            foreach (var turno in turnos)
            {
                Turnos.Add(turno);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
