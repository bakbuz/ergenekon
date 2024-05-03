import { A } from '@solidjs/router';
import logo from '../assets/img/icons8-wolf-96.png'

export default () => {
  return (
    <>
      <footer class="pt-5 bg-mode">
        <div class="container">
          <div class="row g-3">
            <div class="col-sm-6 col-lg-3">
              <div class="d-flex align-middle">
                <A href='/'>
                  <img src={logo} alt="Her pati bir yuva bekler" width="32" />
                  <span class="ms-2 fs-2">Ergenekon</span>
                </A>
              </div>
              <p class="mt-3">Her pati bir yuva bekler</p>
            </div>
            <div class="col-sm-6 col-lg-3">
              <h5 class="mb-4">İndir</h5>
              <ul class="nav flex-column">
                <li class="nav-item"><a class="nav-link" href="javascript:;"><i class="bi bi-apple pe-2"></i>iOS</a></li>
                <li class="nav-item"><a class="nav-link" href="javascript:;"><i class="bi bi-android pe-2"></i>Android</a></li>
                <li class="nav-item"><a class="nav-link" href="javascript:;"><i class="bi bi-windows pe-2"></i>Windows</a></li>
              </ul>
            </div>
            <div class="col-sm-6 col-lg-3">
              <h5 class="mb-4">Bize Ulaşın</h5>
              <ul class="nav flex-column">
                <li class="nav-item"><a class="nav-link" href="st-page-details">Hakkımızda</a></li>
                <li class="nav-item"><a class="nav-link" href="st-page-details">Gizlilik Sözleşmesi</a></li>
                <li class="nav-item"><a class="nav-link" href="javascript:;">Kullanım Koşulları</a></li>
                <li class="nav-item"><a class="nav-link" href="st-iletisim">İletişim</a></li>
              </ul>
            </div>
            <div class="col-sm-6 col-lg-3">
              <h5 class="mb-4">Sosyal medya hesaplarımız</h5>
              <ul class="nav flex-column">
                <li class="nav-item"><a class="nav-link" target="_blank" href="https://facebook.com/patiyuva"><i class="bi bi-facebook pe-2"></i>Facebook</a></li>
                <li class="nav-item"><a class="nav-link" target="_blank" href="https://instagram.com/patiyuvacom"><i class="bi bi-instagram pe-2"></i>Instagram</a></li>
                <li class="nav-item"><a class="nav-link" target="_blank" href="https://twitter.com/patiyuvacom"><i class="bi bi-twitter-x pe-2"></i>Twitter</a></li>
              </ul>
            </div>
          </div>
        </div>
        <hr class="mb-0 mt-5" />
        <div class="bg- light py-3">
          <div class="container">
            <p class="text-center my-1">&copy; 2023 <a class="text-reset" target="_blank" href="https://patiyuva.com"> Ergenekon. </a>Tüm hakları saklıdır.</p>
          </div>
        </div>
      </footer>

      {/* <button type='button' class="icon-md btn btn-primary position-fixed end-0 bottom-0 me-5 mb-5 feedback-button" title='Geri bildirim' onClick={() => window.scrollTo(0, 0)}>
        <i class="bi bi-chat-left-text-fill"></i>
      </button> */}

      <button type='button' class="icon-md btn btn-light position-fixed end-0 bottom-0 me-4 mb-4" title='Yukarı git' onClick={() => window.scrollTo(0, 0)}>
        <i class="bi bi-arrow-up fs-4"></i>
      </button>

    </>
  );
};
