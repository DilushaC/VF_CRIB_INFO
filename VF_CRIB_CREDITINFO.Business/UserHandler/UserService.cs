using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF_CRIB_CREDITINFO.Business.Authentication;
using VF_CRIB_CREDITINFO.Business.ConnectionHandler;
using VF_CRIB_CREDITINFO.Data.Models;

namespace VF_CRIB_CREDITINFO.Business.UserHandler
{
    public class UserService : IUserService
    {
        private readonly _ConnectionService _connectionService;
        private readonly ADAuthentication _aDAuthentication;

        public UserService(_ConnectionService connectionService, ADAuthentication aDAuthentication)
        {
            _connectionService = connectionService;
            _aDAuthentication = aDAuthentication;
        }

        public async Task<UserModel?> ValidateUserAsync(string username, string password)
        {
            // 1. Authenticate AD
            var response = await _aDAuthentication.AuthenticatewithAD(username, password);
            if (!response.Status)
                return null;

            // 2. Get User
            const string userQuery = @"
                SELECT *
                FROM [dbo].[User]
                WHERE EPFNo = @EPFNo AND Active = 1";

            var userParams = new DynamicParameters();
            userParams.Add("@EPFNo", username);

            var userData = _connectionService.ReturnWithPara(userQuery, userParams);
            if (userData == null || userData.Rows.Count == 0)
                return null;

            var userRow = userData.Rows[0];

            var user = new UserModel
            {
                DisplayName = response.Data.DisplayName,
                DisplayDesignation = response.Data.Title,
                DisplayDepartment = response.Data.Department,
            };

            return user;
        }

    }
}
