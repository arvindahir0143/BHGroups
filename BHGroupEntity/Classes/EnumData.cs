using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHGroupEntity.Classes
{
   
    public enum En_CRUD
    {
        Insert,
        Update,
        Delete
    }

    public enum En_UserSession
    {
        oUser,
        id
    }

    public enum En_PloatType
    {
        Villa,
        WeekendHome,
        FarmHouse
    }
   
    public enum En_DrowStatus
    {
        Active,
        Inactive,
        Complete
    }
    public enum En_MemberType
    {
        Broker,
       
        Member
    }
    public enum En_PrefferedZone
    {
        General,

        NRI
    }
    public enum En_FranchiesType
    {
        Franchies,

        Distributers
    }

    public enum En_PaymentType
    {
       Cash,

        Cheque
    }
}
