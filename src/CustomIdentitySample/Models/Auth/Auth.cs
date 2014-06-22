using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace CustomIdentitySample.Models.Auth
{
    /// <summary>
    /// カスタムのユーザー情報
    /// </summary>
    /// <remarks>
    /// IUserを継承し、ユーザー情報として格納したいプロパティを生やしておく。
    /// DB上に格納した、ハッシュ化されたパスワードを保持しておくとよい
    /// </remarks>
    public class MyAppUser : IUser
    {
        /// <summary>
        /// DB上の一意キー
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ログイン認証に使うユーザーID
        /// </summary>
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        /// <summary>
        /// ハッシュ化されてDBに格納されているパスワード
        /// </summary>
        public string HashedPassword { get; set; }
    }

    /// <summary>
    /// カスタムのユーザーストア
    /// </summary>
    /// <remarks>
    /// ユーザー情報のストアにアクセスするための機能を提供。
    /// 『既存の独自のストアによってログインさせる』という機能を提供するだけなら、
    /// 以下の二つのメソッドだけ実装すればとりあえずOK。
    /// -IUserStore.FindByNameAsync : ログイン時に必要なユーザー情報をストアから取得して返す
    /// -IUserPasswordStore.GetPasswordHashAsync : 指定されたユーザーのハッシュ化されたパスワードを返す
    /// （Disposeは例外にならないようにしておく）
    /// </remarks>
    public class MyAppUserStore : IUserStore<MyAppUser>, IUserPasswordStore<MyAppUser>
    {
        public Task<MyAppUser> FindByNameAsync(string userName)
        {
            using (var cn = DbConnectionFactory.Create("DefaultConnection"))
            {
                cn.Open();
                var sql = " SELECT " +
                          "   Convert(nvarchar(MAX), UserID) AS ID , LoginCode AS UserName , FirstName , LastName , EMail , Password AS HashedPassword " +
                          " FROM " +
                          "   UserInfo " +
                          " WHERE " +
                          "   LoginCode = @UserName ";

                var users = cn.Query<MyAppUser>(sql, new { UserName = userName });
                return Task.FromResult(users.FirstOrDefault());
            }
        }

        public Task<string> GetPasswordHashAsync(MyAppUser user)
        {
            return Task.FromResult(user.HashedPassword);
        }

        public Task CreateAsync(MyAppUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyAppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<MyAppUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MyAppUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //例外は出ないようにNotImplementedExceptionは消しておく
        }

        public Task<bool> HasPasswordAsync(MyAppUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(MyAppUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// カスタムのハッシュ生成機
    /// </summary>
    /// <remarks>
    /// パスワードのハッシュアルゴリズムをカスタマイズする場合にIPasswordHasherを実装して作成する
    /// 既存の独自の認証用ストアを利用する場合は必要になりがち。
    /// ASP.NET Identitiyでログインさせるだけなら、とりあえずVerifyHashedPasswordだけ実装すれば動く
    /// </remarks>
    public class MyPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using (var cn = DbConnectionFactory.Create("DefaultConnection"))
            {
                cn.Open();
                var sql = "SELECT dbo.HashPassword(@RawPassword)";
                return cn.Query<string>(sql, new { RawPassword = password }).Single();
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == this.HashPassword(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}