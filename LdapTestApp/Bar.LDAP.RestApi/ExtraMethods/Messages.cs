using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bar.LDAP.RestApi.ExtraMethods
{
    public static class Messages
    {
        public static string MSG_MAIL_SUCCESS = "Mail gönderme işlemi başarılı. Gelen kutusunu kontrol ediniz.";
        public static string MSG_MAIL_ERROR = "Girdiğiniz mail adresi, mail kriterlerine uygun değildir. Lütfen kontrol ediniz..";
        public static string MSG_MAIL_SUBJECT = "Şifre Yenileme Hk.";
        public static string MSG_MAIL_CONTENT = "Kullanım süreniz dolmak üzere. Lütfen parolanızı değiştiriniz..\nŞifre değiştirme ekranı için linke tıklayınız: --> LINK_UPDATE_PAGE";
        public static string MSG_LGN_SUCCESS = "Kullanıcı doğrulaması başarılı..";
        public static string MSG_USER_PWD_ERROR = "Şifre güncelleme işlemi başarısız.";
        public static string MSG_USER_PWD_SUCCESS = "Şifre güncelleme işlemi başarılı. Kullanıcı kontrolü için Login sekmesini veya bilgisayarınızı kullanabilirsiniz.";
        public static string MSG_USER_MAIL_CHECK = "Mail gelen kutusunu kontrol ediniz..";
        public static string MSG_USER_PWD_KEEPGOIN = "Şifreyi kullanmaya devam edebilirsiniz..";
    }
}