using Bookify.Data;
using Bookify.Models;
using Bookify.Response;

using Microsoft.EntityFrameworkCore;

namespace Bookify.Services;

public class UserServices
{ 
    private readonly DataContext _context;

    public UserServices(DataContext context)
    {
        _context = context;
    }

    public ServiceResponse<IEnumerable<User>> GetAllUsers()
    {
        var users = _context.Users.ToList();
        return new ServiceResponse<IEnumerable<User>>()
        {
            Data = users,
            Success = true
        };
    }

    public ServiceResponse<User> CreateUser(User Users)
    {
        var response = new ServiceResponse<User>();

        try
        {
            _context.Users.Add(Users);
            _context.SaveChanges();
            response.Data = Users;
            response.Message = "Usuario Creado Correctamente";
            response.Success = true;


        }
        catch (Exception ex)
        {
            response.Message = "Ingrese un usuario valido";
            response.Success = false;
        }

        return response;

    }


    public ServiceResponse<User> EditUser(int id)
    {
        var response = new ServiceResponse<User>();

        try
        {
            var user = _context.Users.Find(id);
            response.Data = user;
            response.Success = true;
            response.Message = "Usuario Encontrado";
        }
        catch (Exception e)
        {
            response.Message = "No encontrado";
            response.Success = false;
        }

        return response;
    }
    
    public ServiceResponse<User> UpdateUser(User user)
    {
        var response = new ServiceResponse<User>();

        try
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            response.Data = user;
            response.Message = "Usuario Actualizado correctamente";
            response.Success = true;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "No se puedo actualizar usuario";
        }

        return response;
    }


    public ServiceResponse<User> ShowUser(int id)
    {
        var response = new ServiceResponse<User>();

        try
        {
            var user = _context.Users.Find(id);
            response.Data = user;
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "No encontrado";
            response.Success = false;
        }

        return response;
    }


    public ServiceResponse<User> Delete(int id)
    {
        var response = new ServiceResponse<User>();

        try
        {
            var user = _context.Users.Find(id);
            response.Data = user;
            response.Success = true;
            response.Message = "OK";
        }
        catch (Exception e)
        {
            response.Message = "NO se encuentra";
            response.Success = false;
        }
        
        
        return response;
    }
    
    
    
    public ServiceResponse<User>DeleteUser(User user)
    {
        var response = new ServiceResponse<User>();

        try
        {
            _context.Users.Remove(user);
            _context.SaveChanges();

            response.Message = "Usuario eliminado correctamente";
            response.Success = true;
            response.Data = user;

        }
        catch (Exception e)
        {
            response.Message = "No se pudo eliminar";
            response.Success = false;
        }
        
        return response;
    }
 

}