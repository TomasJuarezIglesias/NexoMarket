using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using System.Collections.Generic;

namespace NexoMarket.Business
{
    public class MenuBusiness
    {
        private readonly MenuRepository _menuRepository;
        public MenuBusiness()
        {
            _menuRepository = new MenuRepository();
        }

        public List<MenuEntity> GetMenusByUser(int userId)
        {
            return _menuRepository.GetMenusByUser(userId);
        }
    }
}
