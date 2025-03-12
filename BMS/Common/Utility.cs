using Microsoft.AspNetCore.Identity;

namespace BMS.Common {
    public class Utility {
        public static string[] TimeFormats = {
            "yyyy-MM-dd",
            "yyyy/MM/dd",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "yyyyMMdd",
            "dd-MM-yyyy",
            "M/d/yyyy",  // 添加单个数字月份
            "M/d/yy",    // 添加简写年份
            "MM/dd/yy"   // 添加两位数年份格式
        };

        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        public static string GetEncryptPassword(string str) {
            return passwordHasher.HashPassword(null, str);
        }

        public static bool IsPasswordCorrect(string hashedPassword, string password) {
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }

        public static DateTime GetStartOfMonth(DateTime date) {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DateTime GetEndOfMonth(DateTime date) {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static DateTime GetMinDate() {
            return new DateTime(2025, 1, 1);
        }
        public static DateTime GetMaxDate() {
            return new DateTime(2500, 1, 1);
        }
    }
}
