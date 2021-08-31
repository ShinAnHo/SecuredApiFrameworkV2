using Domain.Api;
using Domain.IdentityServer.Data;
using Domain.Database;
using Domain.IdentityServer.Oauth;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.IdentityServer.Business
{
    public class ClientManager : BaseBusinessLogic, IClient
    {
        public ClientManager(IUnitOfWork uow, IOptions<AppSettings> appSettings, IOptions<ConnectionStrings> connectionStrings) : base(uow, appSettings, connectionStrings) { }
        public async Task<Client> GetById(long ClientId)
        {
            try
            {
                _uow.OpenConnection(ConnectionString.DefaultConnection);

                using (_uow)
                {
                    var repo = new ClientRepository(_uow);
                    var result = await repo.ReadByLambda(b => b.ClientId == ClientId);
                                       
                        if (!result.Any())
                            throw new ApiException("Client", ApiValidationType.NotFound);
                        else
                            return result.First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Client> GetByName(string Name)
        {
            try
            {
                _uow.OpenConnection(ConnectionString.DefaultConnection);

                using (_uow)
                {
                    var repo = new ClientRepository(_uow);
                    var result = await repo.ReadByLambda(b => b.Name == Name && b.IsActive == true);

                        if (!result.Any())
                            throw new ApiException("Client", ApiValidationType.NotFound);
                        else
                            return result.First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Client>> GetAll()
        {
            try
            {
                _uow.OpenConnection(ConnectionString.DefaultConnection);

                using (_uow)
                {
                    var repo = new ClientRepository(_uow);
                    return await repo.ReadAll();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Insert(Client data)
        {
            try
            {
                _uow.OpenConnection(ConnectionString.DefaultConnection);

                using (_uow)
                {
                        var repo = new ClientRepository(_uow); 

                        _uow.BeginTransaction();
                        await repo.Insert(data);
                        _uow.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Client data)
        {
            try
            {
                _uow.OpenConnection(ConnectionString.DefaultConnection);

                using (_uow)
                {
                    var repo = new ClientRepository(_uow);

                    _uow.BeginTransaction();
                        await repo.Update(data);
                        _uow.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Delete(Client data)
        {
            try
            {
                _uow.OpenConnection(ConnectionString.DefaultConnection);

                using (_uow)
                {
                    var repo = new ClientRepository(_uow);

                    _uow.BeginTransaction();
                        await repo.Insert(data);
                        _uow.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ClientSecret> Register(Client data)
        {
            try
            {
                _uow.OpenConnection(ConnectionString.DefaultConnection);

                using (_uow)
                {
                    var clientRepo = new ClientRepository(_uow);
                    var clientSecretRepo = new ClientSecretRepository(_uow);

                    _uow.BeginTransaction();

                    var client = new Client();
                    client.Name = data.Name;
                    client.Type = data.Type;
                    client.DisplayName = data.DisplayName;
                    client.Description = NullHandler.ConvertString(data.Description);
                    client.ClientUri = NullHandler.ConvertString(data.ClientUri); ;
                    client.IsActive = true;
                    client.CreatedBy = "admin";
                    client.CreatedDate = DateTime.Now;

                    var inserted = await clientRepo.Insert(client);
                    var ClientId = long.Parse(inserted.ToString());

                    var clientSecret = new ClientSecret();
                    clientSecret.ClientId = ClientId;
                    clientSecret.Value = OauthTokenManager.GenerateClientSecret();
                    clientSecret.Expiration = DateTime.Now.AddDays(_appSettings.ClientSecretExpiration);
                    clientSecret.IsActive = true;
                    clientSecret.CreatedBy = "admin";
                    clientSecret.CreatedDate = DateTime.Now;

                    await clientSecretRepo.Insert(clientSecret);
                    
                    _uow.CommitTransaction();

                    var clientSecrets = await clientSecretRepo.ReadByLambda(c => c.ClientId == ClientId && c.IsActive == true);
                    return clientSecrets.First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
