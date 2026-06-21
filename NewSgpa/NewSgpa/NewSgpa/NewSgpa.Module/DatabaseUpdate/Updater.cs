using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using Microsoft.Extensions.DependencyInjection;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;
using NewSgpa.Module.BusinessObjects.Prestamos;

namespace NewSgpa.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //EntityObject1 theObject = ObjectSpace.FirstOrDefault<EntityObject1>(u => u.Name == name);
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<EntityObject1>();
            //    theObject.Name = name;
            //}

            // The code below creates users and roles for testing purposes only.
            // In production code, you can create users and assign roles to them automatically, as described in the following help topic:
            // https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication
#if !RELEASE
            // If a role doesn't exist in the database, create this role
            var defaultRole = CreateDefaultRole();
            var adminRole = CreateAdminRole();

            ObjectSpace.CommitChanges(); //This line persists created object(s).

            UserManager userManager = ObjectSpace.ServiceProvider.GetRequiredService<UserManager>();

            // If a user named 'User' doesn't exist in the database, create this user
            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "User") == null)
            {
                // Set a password if the standard authentication type is used
                string EmptyPassword = "";
                _ = userManager.CreateUser<ApplicationUser>(ObjectSpace, "User", EmptyPassword, (user) =>
                {
                    // Add the Users role to the user
                    user.Roles.Add(defaultRole);
                });
            }

            // If a user named 'Admin' doesn't exist in the database, create this user
            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "Admin") == null)
            {
                // Set a password if the standard authentication type is used
                string EmptyPassword = "";
                _ = userManager.CreateUser<ApplicationUser>(ObjectSpace, "Admin", EmptyPassword, (user) =>
                {
                    // Add the Administrators role to the user
                    user.Roles.Add(adminRole);
                });
            }

            ObjectSpace.CommitChanges(); //This line persists created object(s).
#endif
            // Create SGPA-specific roles
            CreateSgpaRoles();
            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
        PermissionPolicyRole CreateAdminRole()
        {
            PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrators");
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
                adminRole.IsAdministrative = true;
            }
            return adminRole;
        }
        PermissionPolicyRole CreateDefaultRole()
        {
            PermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default");
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "StoredPassword", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddObjectPermission<ModelDifference>(SecurityOperations.ReadWriteAccess, "UserId = ToStr(CurrentUserId())", SecurityPermissionState.Allow);
                defaultRole.AddObjectPermission<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, "Owner.UserId = ToStr(CurrentUserId())", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<AuditDataItemPersistent>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddObjectPermissionFromLambda<AuditDataItemPersistent>(SecurityOperations.Read, a => a.UserObject.Key == CurrentUserIdOperator.CurrentUserId().ToString(), SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<AuditEFCoreWeakReference>(SecurityOperations.Read, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }
        void CreateSgpaRoles()
        {
            // SGPA User: read/write on all SGPA entities
            var sgpaUser = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "SGPA Usuario");
            if (sgpaUser == null)
            {
                sgpaUser = ObjectSpace.CreateObject<PermissionPolicyRole>();
                sgpaUser.Name = "SGPA Usuario";
                sgpaUser.AddTypePermissionsRecursively<Afiliado>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaUser.AddTypePermissionsRecursively<Certificacion>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaUser.AddTypePermissionsRecursively<Prestacion>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaUser.AddTypePermissionsRecursively<Receta>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaUser.AddTypePermissionsRecursively<SubsidioCabezal>(SecurityOperations.Read, SecurityPermissionState.Allow);
                sgpaUser.AddTypePermissionsRecursively<Imponible>(SecurityOperations.Read, SecurityPermissionState.Allow);
                // Navigate all SGPA sections
                sgpaUser.AddNavigationPermission("Application/NavigationItems/Items/Afiliados", SecurityPermissionState.Allow);
                sgpaUser.AddNavigationPermission("Application/NavigationItems/Items/Certificaciones", SecurityPermissionState.Allow);
                sgpaUser.AddNavigationPermission("Application/NavigationItems/Items/Prestaciones", SecurityPermissionState.Allow);
                sgpaUser.AddNavigationPermission("Application/NavigationItems/Items/Subsidios", SecurityPermissionState.Allow);
                sgpaUser.AddNavigationPermission("Application/NavigationItems/Items/Aportes", SecurityPermissionState.Allow);
            }

            // SGPA Admin: full access including liquidation
            var sgpaAdmin = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "SGPA Administrador");
            if (sgpaAdmin == null)
            {
                sgpaAdmin = ObjectSpace.CreateObject<PermissionPolicyRole>();
                sgpaAdmin.Name = "SGPA Administrador";
                sgpaAdmin.AddTypePermissionsRecursively<Afiliado>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<Certificacion>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<Prestacion>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<SubsidioCabezal>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<Imponible>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<SpPrestamo>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<SpFactura>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<SpCuota>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<SpPago>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<SpRetencion>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<SpPagoError>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<MapeoAbitab>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<ErrCargaAbitab>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sgpaAdmin.AddTypePermissionsRecursively<Parametros>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                // Navigate all sections
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Afiliados", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Certificaciones", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Prestaciones", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Subsidios", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Aportes", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Préstamos", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Préstamos/Items/SpRetencion_ListView", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Préstamos/Items/SpCuadroAmortizacion_ListView", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Parámetros", SecurityPermissionState.Allow);
                sgpaAdmin.AddNavigationPermission("Application/NavigationItems/Items/Estadísticas", SecurityPermissionState.Allow);
            }
        }
    }
}
