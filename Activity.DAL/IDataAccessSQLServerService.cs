using Activity.DAL;
using Minxtu.DAL.Entity;
using System.Collections.Generic;
using System.Data;

namespace Minxtu.DAL
{
    public interface IDataAccessSQLServerService
    {
        DalResult InsertAdmin(Admin obj);
        DalResult UpdateAdmin(long id, Admin obj);
        DalResult DeleteAdmin(long id);
        Admin SelectAdminById(long id);
        List<Admin> SelectAllAdmin();
        List<Admin> SelectAdminPaged(Admin objFrom, Admin objTo, int pageIndex, int pageSize, out int totalRows);
        List<Admin> SelectAdminPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows);

        DalResult InsertFile(File obj);
        DalResult UpdateFile(long id, File obj);
        DalResult DeleteFile(long id);
        File SelectFileById(long id);
        List<File> SelectAllFile();
        List<File> SelectFilePaged(File objFrom, File objTo, int pageIndex, int pageSize, out int totalRows);
        List<File> SelectFilePaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows);

        DalResult InsertArticle(Article obj);
        DalResult UpdateArticle(long id, Article obj);
        DalResult UpdateArticlePageView(long id, int count);
        DalResult UpdateArticleStatus(long id, byte status);
        DalResult DeleteArticle(long id);
        Article SelectArticleById(long id);
        List<Article> SelectAllArticle();
        List<Article> SelectArticlePaged(Article objFrom, Article objTo, int pageIndex, int pageSize, out int totalRows);

        List<Article> SelectArticlePaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        DalResult InsertConfig(Config obj);
        DalResult UpdateConfig(string key, Config obj);
        DalResult DeleteConfig(string key);
        Config SelectConfigById(string key);
        List<Config> SelectAllConfig();
        List<Config> SelectConfigPaged(Config objFrom, Config objTo, int pageIndex, int pageSize, out int totalRows);

        List<Config> SelectConfigPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        DalResult InsertFeedback(Feedback obj);
        List<Feedback> SelectAllFeedback();
        List<Feedback> SelectFeedbackPaged(Feedback objFrom, Feedback objTo, int pageIndex, int pageSize, out int totalRows);
        List<Feedback> SelectFeedbackPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        DalResult InsertMenu(Menu obj);
        DalResult UpdateMenu(long id, Menu obj);
        DalResult UpdateMenuStatus(long id, byte status);
        DalResult DeleteMenu(long id);
        Menu SelectMenuById(long id);
        List<Menu> SelectAllMenu();
        List<Menu> SelectMenuPaged(Menu objFrom, Menu objTo, int pageIndex, int pageSize, out int totalRows);
        List<Menu> SelectMenuPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows);

        DalResult InsertNewsCategory(NewsCategory obj);
        DalResult UpdateNewsCategory(long id, NewsCategory obj);
        DalResult UpdateNewsCategoryStatus(long id, byte status);
        DalResult DeleteNewsCategory(long id);
        NewsCategory SelectNewsCategoryById(long id);
        
        List<NewsCategory> SelectAllNewsCategory();

        List<NewsCategory> SelectNewsCategoryPaged(NewsCategory objFrom, NewsCategory objTo, int pageIndex, int pageSize,
            out int totalRows);

        List<NewsCategory> SelectNewsCategoryPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        DalResult InsertNews(News obj);
        DalResult UpdateNews(long id, News obj);
        DalResult UpdateNewsStatus(long id, byte status);
        DalResult UpdateNewsPageView(long id, int count);
        DalResult DeleteNews(long id);
        //News SelectNewsById(long id,string cad);
        News SelectNewsById(long id);
        News SelectNewsByUrl(string url);
        //List<News> SelectAllNews(string cad);
        List<News> SelectAllNews();
        List<News> SelectNewsPaged(News objFrom, News objTo, int pageIndex, int pageSize, out int totalRows,string cad);
        List<News> SelectNewsPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows,string cad);

        DalResult InsertProductCategory(ProductCategory obj);
        DalResult UpdateProductCategory(long id, ProductCategory obj);
        DalResult UpdateProductCategoryStatus(long id, byte status);
        DalResult DeleteProductCategory(long id);
        ProductCategory SelectProductCategoryById(long id);
        List<ProductCategory> SelectAllProductCategory();

        List<ProductCategory> SelectProductCategoryPaged(ProductCategory objFrom, ProductCategory objTo, int pageIndex,
            int pageSize, out int totalRows);

        List<ProductCategory> SelectProductCategoryPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        List<ProductCategory> SelectSubProductCategoryByParentId(long parentId);

        DalResult InsertProduct(Product obj);
        DalResult UpdateProduct(long id, Product obj);
        DalResult UpdateProductPageView(long id, int count);
        DalResult UpdateProductStatus(long id, byte status);
        DalResult DeleteProduct(long id);
        Product SelectProductById(long id);
        List<Product> SelectAllProduct();
        List<Product> SelectProductTop(int top);
        List<Product> SelectProductPaged(Product objFrom, Product objTo, int pageIndex, int pageSize, out int totalRows);

        List<Product> SelectProductPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        List<Product> SelectProductPagedForAdmin(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);


        DalResult InsertWebAd(WebAd obj);
        DalResult UpdateWebAd(long id, WebAd obj);
        DalResult DeleteWebAd(long id);
        WebAd SelectWebAdById(long id);
        List<WebAd> SelectAllWebAd();
        List<WebAd> SelectWebAdPaged(WebAd objFrom, WebAd objTo, int pageIndex, int pageSize, out int totalRows);
        List<WebAd> SelectWebAdPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows);
        DalResult InsertWebLink(WebLink obj);
        DalResult UpdateWebLink(long id, WebLink obj);
        DalResult DeleteWebLink(long id);
        WebLink SelectWebLinkById(long id);
        List<WebLink> SelectAllWebLink();
        List<WebLink> SelectWebLinkPaged(WebLink objFrom, WebLink objTo, int pageIndex, int pageSize, out int totalRows);

        List<WebLink> SelectWebLinkPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        DalResult InsertTag(Tag obj);
        DalResult UpdateTag(long id, Tag obj);
        DalResult DeleteTag(long id);
        Tag SelectTagById(long id);
        Tag SelectTagByTagName(string tagname);
        List<Tag> SelectAllTag();
        List<Tag> SelectTagPaged(Tag objFrom, Tag objTo, int pageIndex, int pageSize, out int totalRows);
        List<Tag> SelectTagPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows);

        DalResult InsertKeywordLink(KeywordLink obj);
        DalResult UpdateKeywordLink(long id, KeywordLink obj);
        DalResult DeleteKeywordLink(long id);
        KeywordLink SelectKeywordLinkById(long id);
        List<KeywordLink> SelectAllKeywordLink();

        List<KeywordLink> SelectKeywordLinkPaged(KeywordLink objFrom, KeywordLink objTo, int pageIndex, int pageSize,
            out int totalRows);

        List<KeywordLink> SelectKeywordLinkPaged(string whereClause, string orderBy, int pageIndex, int pageSize,
            out int totalRows);

        List<ProductColor> SelectAllProductColor();
        List<ProductMaterial> SelectAllProductMaterial();

        DataSet BackupDatabase();

        bool RestoreDatabase(DataSet ds);

        DalResult InsertAlbum(Album obj);
        DalResult UpdateAlbum(long id, Album obj);
        DalResult DeleteAlbum(long id);
        Album SelectAlbumById(long id);
        List<Album> SelectAllAlbum();
        List<Album> SelectAlbumPaged(Album objFrom, Album objTo, int pageIndex, int pageSize, out int totalRows);
        List<Album> SelectAlbumPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows);

        DalResult InsertPhoto(Photo obj);
        DalResult UpdatePhoto(long id, Photo obj);
        DalResult DeletePhoto(long id);
        Photo SelectPhotoById(long id);

        List<Photo> SelectPhotoByAlbumId(long albumId);
        List<Photo> SelectAllPhoto();
        List<Photo> SelectPhotoPaged(Photo objFrom, Photo objTo, int pageIndex, int pageSize, out int totalRows);
        List<Photo> SelectPhotoPaged(string whereClause, string orderBy, int pageIndex, int pageSize, out int totalRows);



        void BuildNewUrlRewrite(News news, string cad);
        void BuildArticleUrlRewrite(Article article, string cad);

    }

}
