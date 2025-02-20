mergeInto(LibraryManager.library,
{
	InitPayments_js: function()
	{
		var returnStr = paymentsData;
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
	},
	
	GetPayments_js: function()
	{
		GetPayments();
	},
	
	ConsumePurchase_js: function(id)
	{
		ConsumePurchase(UTF8ToString(id));
	},
	
	ConsumePurchase_js: function()
	{
		ConsumePurchases();
	},
	
	BuyPayments_js: function(id)
	{
		BuyPayments(UTF8ToString(id));
	}
});



mergeInto(LibraryManager.library, {
	BayNoAds: function ()
	{
	  payments.purchase({ id: 'NoAds' }).then(purchase => {
			  console.log('Покупка успешно совершена!');
		myGameInstance.SendMessage("InApp", "BayTrue");
		  }).catch(err => {
			  console.log('Покупка не удалась :(');
		  })
	},
  
	HasBayNoAds: function()
	{
	  payments.getPurchases().then(purchases => {
		if (purchases.some(purchase => purchase.productID === 'NoAds')) {
		  myGameInstance.SendMessage("InApp", "BayTrue");
		}
	  }).catch(err => {
		// Выбрасывает исключение USER_NOT_AUTHORIZED для неавторизованных пользователей.
	  })
	},
  
	DeleteNoAds: function(){
	  payments.purchase({ id: 'NoAds' }).then(purchase => {
		// Покупка успешно совершена!
		payments.consumePurchase(purchase.purchaseToken);
		myGameInstance.SendMessage("InApp", "DeleteBayTrue");
	  });
	}
  });
  