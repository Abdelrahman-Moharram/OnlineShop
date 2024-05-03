
namespace OnlineShop.Core.Constants
{
    public class RoleModules
    {
        private readonly string[] _CrudsArr;

        private readonly Dictionary<string, string[]> _roleCruds;

        public RoleModules()
        {
            _CrudsArr = Enum
                .GetNames(typeof(Cruds))
                .Cast<string>()
                .ToArray();


            _roleCruds = new Dictionary<string, string[]>()
            {
                // Admins
                {$"{Roles.Admin}.{Modules.Product}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString(), Cruds.Delete.ToString()] },
                {$"{Roles.Admin}.{Modules.ProductItem}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString(), Cruds.Delete.ToString()] },
                {$"{Roles.Admin}.{Modules.Brand}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString(), Cruds.Delete.ToString()] },
                {$"{Roles.Admin}.{Modules.Category}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString(), Cruds.Delete.ToString()] },
                {$"{Roles.Admin}.{Modules.Order}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.OrderItem}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.SiteSettings}",[Cruds.Read.ToString()] },
                {$"{Roles.Admin}.{Modules.Cart}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Delete.ToString()] },
                {$"{Roles.Admin}.{Modules.CartItem}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.Accounts}",[Cruds.Read.ToString()] },


                // Basic

                {$"{Roles.Basic}.{Modules.Product}",[Cruds.Read.ToString()] },
                {$"{Roles.Basic}.{Modules.ProductItem}",[Cruds.Read.ToString()] },
                {$"{Roles.Basic}.{Modules.Brand}",[Cruds.Read.ToString()] },
                {$"{Roles.Basic}.{Modules.Category}",[Cruds.Read.ToString()] },
                {$"{Roles.Basic}.{Modules.Order}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Basic}.{Modules.OrderItem}",[Cruds.Read.ToString()] },
                {$"{Roles.Basic}.{Modules.SiteSettings}",[] },
                {$"{Roles.Basic}.{Modules.Cart}",[Cruds.Read.ToString(), Cruds.Update.ToString()] },
                {$"{Roles.Basic}.{Modules.CartItem}",[Cruds.Read.ToString(), Cruds.Update.ToString()] },
                {$"{Roles.Basic}.{Modules.Accounts}",[] },


            };
        }

        private static RoleModules _lock = new();
        private static RoleModules _instance = new();
        public static RoleModules instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new();
                    return _instance;
                }
            }
        }

        public string[] cruds(string roleName, string Module)
        {
            if (roleName == Roles.SuperAdmin.ToString())
                return _CrudsArr;
            return _roleCruds[$"{roleName}.{Module}"];
        }
    }
}
