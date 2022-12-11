using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SistemaDeTarefasDBContext _context;

        public UsuarioRepository(SistemaDeTarefasDBContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            var usuarioModel = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuarioModel == null)
            {
                throw new Exception($"Usuario para o ID {id} não foi encontrado.");
            }
            return usuarioModel;
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            var usuarioModel = await BuscarPorId(id);

            usuarioModel.Nome = usuario.Nome;
            usuarioModel.Email = usuario.Email;

            //await _context.Usuarios.UpdateAsync(usuario);
            await _context.SaveChangesAsync();

            return usuarioModel;
        }

        public async Task<bool> Apagar(int id)
        {
            var usuarioModel = await BuscarPorId(id);

            _context.Usuarios.Remove(usuarioModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}