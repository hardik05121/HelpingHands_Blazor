﻿namespace HelpingHands_Business.Repository.IRepostiory
        IAmenityRepository Amenity { get; }

        ISecondCategoryRepository SecondCategory { get; }
        IServiceRepository Service { get; }
        IThirdCategoryRepository ThirdCategory { get; }

        IApplicationUserRepository ApplicationUser { get; }
        IApplicationRoleRepository ApplicationRole { get; }
        IApplicationUserRoleRepository ApplicationUserRole { get; }
        IUserRepository User { get; }


        void Save();