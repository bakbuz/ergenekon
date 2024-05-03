export default () => {
	return (

		<main>

			<div class="container">
				<div class="row justify-content-center align-items-center vh-100 py-5">
					<div class="col-sm-10 col-md-8 col-lg-7 col-xl-6 col-xxl-5">
						<div class="card card-body rounded-3 text-center p-4 p-sm-5">
							<h1 class="mb-2">Parolanı sıfırla</h1>
							<p>Yeni parola belirle.</p>
							<form class="mt-3">
								<div class="mb-3 position-relative">
									<div class="input-group input-group-lg">
										<input class="form-control fakepassword" type="password" id="psw-input" placeholder="Yeni parola" required />
										<span class="input-group-text p-0">
											<i class="fakepasswordicon bi bi-eye-slash cursor-pointer p-2 w-40px"></i>
										</span>
									</div>
								</div>

								<div class="mb-3">
									<input class="form-control" type="password" placeholder="Yeni parolanı onayla" required />
								</div>
								<div class="d-grid"><button type="submit" class="btn btn-lg btn-primary">Sıfırla</button></div>

								<p class="mb-0 mt-3">&copy; 2023 <A target="_blank" href="https://patiyuva.com">Patiyuva.</A> Tüm hakları saklıdır.</p>
							</form>
						</div>
					</div>
				</div>
			</div>

		</main>

	);
};
