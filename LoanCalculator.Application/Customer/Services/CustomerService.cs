using LoanCalculator.Application.Customer.Dtos;
using LoanCalculator.Application.Customer.Interfaces;
using LoanCalculator.Core.Specification;
using LoanCalculator.Common;
using LoanCalculator.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Customer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<CustomerService> _logger;

        public CustomerService(IUnitOfWork unitOfWork,
                               IAppLogger<CustomerService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id, cancellationToken);

            return new CustomerViewModel
            {
                Id = customer.Id,
                Title = customer.Title,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Mobile = customer.Mobile,
                DateCreated = customer.DateCreated,
                DateLastModified = customer.DateLastModified
            };
        }

        public async Task<CustomerViewModel> AuthentiateUserAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;

            var customerSpecification = new CustomerSpecification(username);
            var customer = await _unitOfWork.CustomerRepository.AuthenticateAsync(customerSpecification);

            if (customer == null)
                return null;

            if (!Helper.VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
                return null;


            return new CustomerViewModel
            {
                Id = customer.Id,
                Title = customer.Title,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Mobile = customer.Mobile,
                UserName = customer.UserName,
                Password = "***",
                DateCreated = customer.DateCreated,
                DateLastModified = customer.DateLastModified
            };
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomersAsync(CancellationToken cancellationToken = default)
        {
            var customers = await _unitOfWork.CustomerRepository.ListAllAsync(cancellationToken);

            return customers.Select(customer => new CustomerViewModel
            {
                Id = customer.Id,
                Title = customer.Title,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                Email = customer.Email,
                Mobile = customer.Mobile,
                DateCreated = customer.DateCreated,
                DateLastModified = customer.DateLastModified
            });
        }

        public async Task<CustomerViewModel> CreateCustomersAsync(CustomerPostModel createModel, CancellationToken cancellationToken = default)
        {
            Helper.CreatePasswordHash(createModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var customer = await _unitOfWork.CustomerRepository.AddAsync(new Core.Entities.Customer()
            {
                Title = createModel.Title,
                FirstName = createModel.FirstName,
                LastName = createModel.LastName,
                Email = createModel.Email,
                Mobile = createModel.Mobile,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DateCreated = System.DateTime.UtcNow
            }, cancellationToken);

            if (await _unitOfWork.SaveAsync(cancellationToken) > 0)
            {
                return new CustomerViewModel
                {
                    Id = customer.Id,
                    Title = customer.Title,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    UserName = customer.UserName,
                    Password = "****",
                    Email = customer.Email,
                    Mobile = customer.Mobile,
                    DateCreated = customer.DateCreated
                };
            }

            return null;
        }

        public async Task<bool> UpdateCustomersAsync(CustomerPostModel updateModel, CancellationToken cancellationToken = default)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(updateModel.Id, cancellationToken);

            if (customer != null)
            {
                customer.Title = updateModel.Title;
                customer.FirstName = updateModel.FirstName;
                customer.LastName = updateModel.LastName;
                customer.Email = updateModel.Email;
                customer.Mobile = updateModel.Mobile;
                customer.DateLastModified = System.DateTime.UtcNow;

                await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
            }

            return await _unitOfWork.SaveAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteCustomersAsync(long id, CancellationToken cancellationToken = default)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id, cancellationToken);

            if (customer != null)
            {
                await _unitOfWork.CustomerRepository.DeleteAsync(customer, cancellationToken);
            }

            return await _unitOfWork.SaveAsync(cancellationToken) > 0;
        }
    }
}
