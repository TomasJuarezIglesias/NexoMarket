using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using System.Collections.Generic;

namespace NexoMarket.Business
{
    public class BusinessMenu
    {
        private readonly MenuRepository _menuRepository;
        public BusinessMenu()
        {
            _menuRepository = new MenuRepository();
        }

        public List<MenuEntity> GetMenusByUser(int userId)
        {
            return _menuRepository.GetMenusByUser(userId);
        }
    }
}
