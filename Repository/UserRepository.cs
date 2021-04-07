using Google.Api.Gax;
using Google.Cloud.Firestore;
using PirlantaApi.Entities;
using PirlantaApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirlantaApi.Repository
{
    public class UserRepository : IUserRepository
    {
        FirestoreDb db = new FirestoreDbBuilder
        {
            ProjectId = AppConstants.ProjectId,
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction,

        }.Build();
        IMagazaRepository _magazaRepository;
        public UserRepository(IMagazaRepository magazaRepository)
        {
            _magazaRepository = magazaRepository;
        }

        public async Task DeleteUser(User user)
        {
            DocumentReference docRef = UserDocument(user.Uid);
            await docRef.DeleteAsync();
            Magaza magaza = await _magazaRepository.GetMagazaByUid(user.Uid);
            await _magazaRepository.DeleteMagaza(magaza);
        }

        public async Task<User> GetUser(string uid)
        {
            DocumentReference docRef = UserDocument(uid);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            var user = snapshot.ConvertTo<User>();
            return user;
        }

        public async Task PostUser(User user)
        {
            await UserDocument(user.Uid).SetAsync(user);
        }

        public async Task PutUser(User user)
        {
            DocumentReference docRef =UserDocument(user.Uid);
            await docRef.SetAsync(user);
        }
        public async Task<bool> UserExists(string id)
        {
            if (await GetUser(id) != null) return true;
            return false;
        }

        private CollectionReference UsersCollection()
        {
            return db.Collection("users");
        }

        private DocumentReference UserDocument(string uid)
        {
            return db.Collection("users").Document(uid);
        }
    }
}
