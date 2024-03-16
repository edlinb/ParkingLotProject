using ParkingLotProject.Models;

namespace ParkingLotProject.Repositories
{
    public class SubscriberRepository
    {
        private readonly ParkingDbContext _context;

        public SubscriberRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public int GetActiveSubsriberCount()
        {
            return _context.Subscriber.Count(subscriber => !subscriber.isDeleted);
        }
        public void CreateSubscriber(Subscriber newSubscriber)
        {
            bool exists = _context.Subscriber.Any(subscriber => subscriber.IdCard == newSubscriber.IdCard);

            if (exists)
            {
                throw new Exception("A subscriber with the same Id card number already exists");
            }
            _context.Subscriber.Add(newSubscriber);
            _context.SaveChanges();
        }
        public IEnumerable<Subscriber> GetAllSubscribers()
        {
            return _context.Subscriber.ToList();
        }
        public void UpdateSubscriber(Subscriber updatedSubscriber)
        {
            var existingSubscriber = _context.Subscriber.FirstOrDefault(subscriber => subscriber.IdCard == updatedSubscriber.IdCard);
            if (existingSubscriber != null)
            {

                throw new Exception("Subscriber not found");
            }
            existingSubscriber.FirstName = updatedSubscriber.FirstName;
            existingSubscriber.LastName = updatedSubscriber.LastName;
            existingSubscriber.Email = updatedSubscriber.Email;
            existingSubscriber.Phone = updatedSubscriber.Phone;
            existingSubscriber.PlateNumber = updatedSubscriber.PlateNumber;
            existingSubscriber.isDeleted = updatedSubscriber.isDeleted;
            _context.SaveChanges();
        }
        public Subscriber GetSubscriberByIdCard(int idCard)
        {
            return _context.Subscriber.FirstOrDefault(subscriber => subscriber.IdCard == idCard);
        }
        public void SoftDeletedSubscriber(int IdCard)
        {
            var subscriber = _context.Subscriber.FirstOrDefault(sub => sub.IdCard == IdCard);

            if (subscriber != null)
            {
                subscriber.isDeleted = true;
                _context.SaveChanges();

            }
            else
            {
                throw new Exception("Subscriber not found.");

            }
        }

    }
}
