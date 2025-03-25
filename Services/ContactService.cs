using GroupMailer.DTO;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupMailer.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactCommandRepository _contactCommandRepository;
        private readonly IContactQueryRepository _contactQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;

        public ContactService(IContactCommandRepository contactCommandRepository, IContactQueryRepository contactQueryRepository, IUserQueryRepository userQueryRepository)
        {
            _contactCommandRepository = contactCommandRepository;
            _contactQueryRepository = contactQueryRepository;
            _userQueryRepository = userQueryRepository;
        }

        public async Task<string> AddExistingContactAsync(int userId, int contactId)
        {
            await _contactCommandRepository.AddContactAsync(userId, contactId);
            return "Contact added";
        }

        public async Task<string> AddNewContactAsync(Contact contact, string email)
        {
            var user = await _userQueryRepository.GetByEmailAsync(email);

            await _contactCommandRepository.AddNewContactAsync(contact, user.Id);
            return "Contact added";
        }

        public async Task<bool> DeleteContactAsync(int userId, int contactId)
        {
            var result = await _contactCommandRepository.DeleteContactUserByUserIdAndContactIdAsync(userId, contactId);
            return result == 1;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _contactQueryRepository.GetAllAsync();
        }

        public async Task<List<Contact>> GetAllOtherCOntactAsync(int UserId)
        {
            return await _contactQueryRepository.GetAllOtherCOntactAsync(UserId);
        }

        public async Task<List<Contact>> GetAllUserContactAsync(int UserId)
        {
            return await _contactQueryRepository.GetAllUserContactAsync(UserId);
        }

        public async Task<Contact> GetContactByIdAsync(int UserId)
        {
            return await _contactQueryRepository.GetContactByIdAsync(UserId);
        }

        public async Task<string> ModifyConctactAsync(ContactUpdateDto contactDto)
        {
            var contact = await _contactQueryRepository.GetContactByIdAsync(contactDto.Id);
            if (contact == null)
            {
                return "Not Found";
            }

            if (!string.IsNullOrEmpty(contactDto.Name))
            {
                contact.Name = contactDto.Name;
            }

            if (!string.IsNullOrEmpty(contactDto.CustomName))
            {
                contact.CustomName = contactDto.CustomName;
            }

            if (!string.IsNullOrEmpty(contactDto.Email))
            {
                contact.Email = contactDto.Email;
            }

            await _contactCommandRepository.UpdateContactAsync(contact);
            return "Contact updated";
        }
    }
}
