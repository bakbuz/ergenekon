import { A } from "@solidjs/router";

export default () => {
	return (
		<main>
			<div class="container">
				<div class="row justify-content-center align-items-center vh-100 py-5">
					<div class="col-sm-10 col-md-8 col-lg-7 col-xl-6 col-xxl-5">
						<div class="card card-body rounded-3 text-center p-4 p-sm-5">
							<h1 class="mb-2">Parolanı kurtar</h1>
							<p>Parolanı kurtarabilmen için sıfırlama bağlantısı içeren bir e-posta yollayacağız. Lütfen kayıtlı e-posta adresini gir.</p>
							<form class="mt-3">
								<div class="mb-3">
									<div class="input-group input-group-lg">
										<input class="form-control fakepassword" type="password" id="psw-input" placeholder="E-posta adresin" required />
									</div>
								</div>
								<div class="d-grid"><button type="submit" class="btn btn-lg btn-primary">Kurtarma iletisi gönder</button></div>
								<div class="my-4">
									<p>Giriş sayfasına dönmek için <A href="/giris">buraya tıklayın</A></p>
								</div>
								<p class="mb-0 mt-3">&copy; 2023&nbsp; <a target="_blank" href="https://ergenekon.com">Ergenekon.</a> Tüm hakları saklıdır.</p>
							</form>
						</div>
					</div>
				</div>
			</div>
		</main>
	);
};
