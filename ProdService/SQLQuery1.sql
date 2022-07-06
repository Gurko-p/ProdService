use ProdServiceDB

select dcmj.MenuItemId, pdcj.ProductId,  Sum(pdcj.WeightBrutto) from DishCartMenuItemJunctions as dcmj
join ProductDishCartJunctions as pdcj on dcmj.DishCartId = pdcj.DishCartId
group by dcmj.MenuItemId, pdcj.ProductId, pdcj.WeightBrutto

