using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loadi.Models
{
    public class RegisterMobdel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public TimeJoinMarketEnum TimeJoinMarket { get; set; }

        public TimeJoinMarketEnum TimePlan { get; set; }

        public LevelRiskEnum LevelRisk { get; set; }

        public InvesmentAmountEnum InvesmentAmount { get; set; }

        public short InvesmentWay { get; set; }


    }

    public enum TimeJoinMarketEnum
    {
        One = 1, // one year
        Two = 2, // two year
        Three = 3 // there year
    }

    public enum LevelRiskEnum
    {
        Low = 1, // Thấp,
        Normal = 2, // Trung bình
        High = 3 // CaoB
    }

    public enum InvesmentAmountEnum
    {
        Max300 = 1,// Dưới 300 triệu
        F300T500 = 2,//Từ 300 - 500 triệu
        F500T1M = 3,//Từ 500 - 1 tỷ
        F1M = 4//Trên 1 tỷ
    }

    public enum InvesmentWayEnum
    {
        Way1 = 1,//Chia sẻ 20% lợi nhuận cho nhà quản lý đầu tư
        Way2 = 2,//Cam kết không hưởng phí quản lý nếu tỷ suất sinh lãi thấp hơn 10%
        Way3 = 3//Chia sẻ rủi ro với khách hàng
    }


}