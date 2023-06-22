﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ergenekon.Infrastructure.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Ergenekon.Infrastructure.Localization.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to İyimser eşzamanlılık hatası, nesne değiştirildi..
        /// </summary>
        internal static string ConcurrencyFailure {
            get {
                return ResourceManager.GetString("ConcurrencyFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bilinmeyen bir sorun oluştu..
        /// </summary>
        internal static string DefaultError {
            get {
                return ResourceManager.GetString("DefaultError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to E-posta &apos;{0}&apos; zaten alınmış..
        /// </summary>
        internal static string DuplicateEmail {
            get {
                return ResourceManager.GetString("DuplicateEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rol adı &apos;{0}&apos; zaten alınmış..
        /// </summary>
        internal static string DuplicateRoleName {
            get {
                return ResourceManager.GetString("DuplicateRoleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı adı &apos;{0}&apos; zaten alınmış..
        /// </summary>
        internal static string DuplicateUserName {
            get {
                return ResourceManager.GetString("DuplicateUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to E-posta &apos;{0}&apos; geçersiz..
        /// </summary>
        internal static string InvalidEmail {
            get {
                return ResourceManager.GetString("InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} türü {1}&lt;{2}&gt;&apos;den türetilmelidir..
        /// </summary>
        internal static string InvalidManagerType {
            get {
                return ResourceManager.GetString("InvalidManagerType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sağlanan PasswordHasherCompatibilityMode geçersiz..
        /// </summary>
        internal static string InvalidPasswordHasherCompatibilityMode {
            get {
                return ResourceManager.GetString("InvalidPasswordHasherCompatibilityMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Yineleme sayısı pozitif bir tamsayı olmalıdır..
        /// </summary>
        internal static string InvalidPasswordHasherIterationCount {
            get {
                return ResourceManager.GetString("InvalidPasswordHasherIterationCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rol adı &apos;{0}&apos; geçersiz..
        /// </summary>
        internal static string InvalidRoleName {
            get {
                return ResourceManager.GetString("InvalidRoleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Geçersiz belirteç (token)..
        /// </summary>
        internal static string InvalidToken {
            get {
                return ResourceManager.GetString("InvalidToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı adı &apos;{0}&apos; geçersizdir, yalnızca harf ya da rakam içerebilir..
        /// </summary>
        internal static string InvalidUserName {
            get {
                return ResourceManager.GetString("InvalidUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bu hesaba bağlanmış bir kullanıcı zaten var..
        /// </summary>
        internal static string LoginAlreadyAssociated {
            get {
                return ResourceManager.GetString("LoginAlreadyAssociated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AddIdentity hizmet koleksiyonu üzerinde çağrılmalıdır..
        /// </summary>
        internal static string MustCallAddIdentity {
            get {
                return ResourceManager.GetString("MustCallAddIdentity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IPersonalDataProtector hizmeti kaydedilmedi, ProtectPersonalData = true olduğunda bu gereklidir..
        /// </summary>
        internal static string NoPersonalDataProtector {
            get {
                return ResourceManager.GetString("NoPersonalDataProtector", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rol türü belirtilmedi, AddRoles&lt;TRole&gt;() işlevini deneyin..
        /// </summary>
        internal static string NoRoleType {
            get {
                return ResourceManager.GetString("NoRoleType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{1}&apos; adında bir IUserTwoFactorTokenProvider&lt;{0}&gt; kayıtlı değil..
        /// </summary>
        internal static string NoTokenProvider {
            get {
                return ResourceManager.GetString("NoTokenProvider", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı güvenlik damgası boş olamaz..
        /// </summary>
        internal static string NullSecurityStamp {
            get {
                return ResourceManager.GetString("NullSecurityStamp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Yanlış parola..
        /// </summary>
        internal static string PasswordMismatch {
            get {
                return ResourceManager.GetString("PasswordMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parola en az bir rakam (&apos;0&apos;-&apos;9&apos;) içermelidir..
        /// </summary>
        internal static string PasswordRequiresDigit {
            get {
                return ResourceManager.GetString("PasswordRequiresDigit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parola en az bir küçük harf (&apos;a&apos;-&apos;z&apos;) içermelidir..
        /// </summary>
        internal static string PasswordRequiresLower {
            get {
                return ResourceManager.GetString("PasswordRequiresLower", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parola en az bir sembol içermelidir. (&apos;!&apos;, &apos;+&apos; vb).
        /// </summary>
        internal static string PasswordRequiresNonAlphanumeric {
            get {
                return ResourceManager.GetString("PasswordRequiresNonAlphanumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parola en az {0} farklı karakter içermelidir..
        /// </summary>
        internal static string PasswordRequiresUniqueChars {
            get {
                return ResourceManager.GetString("PasswordRequiresUniqueChars", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parola en az bir büyük harf (&apos;A&apos;-&apos;Z&apos;) içermelidir..
        /// </summary>
        internal static string PasswordRequiresUpper {
            get {
                return ResourceManager.GetString("PasswordRequiresUpper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parola uzunluğu en az {0} karakter olmalıdır..
        /// </summary>
        internal static string PasswordTooShort {
            get {
                return ResourceManager.GetString("PasswordTooShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kurtarma kodu kullanımı başarısız oldu..
        /// </summary>
        internal static string RecoveryCodeRedemptionFailed {
            get {
                return ResourceManager.GetString("RecoveryCodeRedemptionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} rolü mevcut değil..
        /// </summary>
        internal static string RoleNotFound {
            get {
                return ResourceManager.GetString("RoleNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, ProtectPersonalData = true olduğunda gerekli olan IProtectedUserStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIProtectedUserStore {
            get {
                return ResourceManager.GetString("StoreNotIProtectedUserStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IQueryableRoleStore&lt;TRole&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIQueryableRoleStore {
            get {
                return ResourceManager.GetString("StoreNotIQueryableRoleStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IQueryableUserStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIQueryableUserStore {
            get {
                return ResourceManager.GetString("StoreNotIQueryableUserStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IRoleClaimStore&lt;TRole&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIRoleClaimStore {
            get {
                return ResourceManager.GetString("StoreNotIRoleClaimStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserAuthenticationTokenStore&lt;User&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserAuthenticationTokenStore {
            get {
                return ResourceManager.GetString("StoreNotIUserAuthenticationTokenStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserAuthenticatorKeyStore&lt;User&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserAuthenticatorKeyStore {
            get {
                return ResourceManager.GetString("StoreNotIUserAuthenticatorKeyStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserClaimStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserClaimStore {
            get {
                return ResourceManager.GetString("StoreNotIUserClaimStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserConfirmationStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserConfirmationStore {
            get {
                return ResourceManager.GetString("StoreNotIUserConfirmationStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserEmailStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserEmailStore {
            get {
                return ResourceManager.GetString("StoreNotIUserEmailStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserLockoutStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserLockoutStore {
            get {
                return ResourceManager.GetString("StoreNotIUserLockoutStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserLoginStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserLoginStore {
            get {
                return ResourceManager.GetString("StoreNotIUserLoginStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserPasswordStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserPasswordStore {
            get {
                return ResourceManager.GetString("StoreNotIUserPasswordStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserPhoneNumberStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserPhoneNumberStore {
            get {
                return ResourceManager.GetString("StoreNotIUserPhoneNumberStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserRoleStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserRoleStore {
            get {
                return ResourceManager.GetString("StoreNotIUserRoleStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserSecurityStampStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserSecurityStampStore {
            get {
                return ResourceManager.GetString("StoreNotIUserSecurityStampStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserTwoFactorRecoveryCodeStore&lt;User&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserTwoFactorRecoveryCodeStore {
            get {
                return ResourceManager.GetString("StoreNotIUserTwoFactorRecoveryCodeStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Store, IUserTwoFactorStore&lt;TUser&gt; öğesini uygulamıyor..
        /// </summary>
        internal static string StoreNotIUserTwoFactorStore {
            get {
                return ResourceManager.GetString("StoreNotIUserTwoFactorStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcının zaten belirlenmiş bir parolası var..
        /// </summary>
        internal static string UserAlreadyHasPassword {
            get {
                return ResourceManager.GetString("UserAlreadyHasPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı zaten &apos;{0}&apos; rolündedir..
        /// </summary>
        internal static string UserAlreadyInRole {
            get {
                return ResourceManager.GetString("UserAlreadyInRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı hesabı kilitlendi..
        /// </summary>
        internal static string UserLockedOut {
            get {
                return ResourceManager.GetString("UserLockedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bu kullanıcı için kilitleme etkin değil..
        /// </summary>
        internal static string UserLockoutNotEnabled {
            get {
                return ResourceManager.GetString("UserLockoutNotEnabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı {0} mevcut değil..
        /// </summary>
        internal static string UserNameNotFound {
            get {
                return ResourceManager.GetString("UserNameNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı &apos;{0}&apos; rolünde değil..
        /// </summary>
        internal static string UserNotInRole {
            get {
                return ResourceManager.GetString("UserNotInRole", resourceCulture);
            }
        }
    }
}
