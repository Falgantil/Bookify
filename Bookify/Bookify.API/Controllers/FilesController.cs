using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bookify.API.Attributes;
using Bookify.API.Provider;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bookify.API.Controllers.BaseApiController" />
    [RoutePrefix("Files")]
    public class FilesController : BaseApiController
    {
        private readonly IFileServerRepository _fileServerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesController"/> class.
        /// </summary>
        /// <param name="fileServerRepository">The file server repository.</param>
        public FilesController(IFileServerRepository fileServerRepository)
        {
            this._fileServerRepository = fileServerRepository;
        }

        #region Epub

        /// <summary>
        /// Uploads the epub.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="UploadEpub">OK</response>
        /// <response code="400">Bad Reqeust Error</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        [Route("{id}/epub")]
        public async Task<IHttpActionResult> UploadEpub(int id)
        {
            return await this.Try(
                async () =>
                {
                    // Check if the request contains multipart/form-data
                    if (!this.Request.Content.IsMimeMultipartContent("form-data"))
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                    var provider = await this.Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

                    //access files
                    IList<HttpContent> files = provider.Files;

                    //Example: reading a file's stream like below
                    HttpContent file = files[0];
                    Stream fileStream = await file.ReadAsStreamAsync();

                    await this._fileServerRepository.SaveEpubFile(id, fileStream);
                });
        }

        /// <summary>
        /// Downloads the epub.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="DownloadEpub">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Auth]
        [Route("{id}/epub")]
        [ResponseType(typeof(ByteArrayContent))]
        public async Task<IHttpActionResult> DownloadEpub(int id)
        {
            return await this.TryRaw(
                async () =>
                {
                    var file = await this._fileServerRepository.GetEpubFile(id);

                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content =
                                    new ByteArrayContent(file.ToArray())
                                    {
                                        Headers = { ContentType = new MediaTypeHeaderValue("image/png") }
                                    }
                    };
                    return this.ResponseMessage(response);
                });
        }

        /// <summary>
        /// Replaces the epub.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="ReplaceEpub">OK</response>
        /// <returns></returns>
        [HttpPut]
        [Auth]
        [Route("{id}/epub")]
        public async Task<IHttpActionResult> ReplaceEpub(int id)
        {
            return await this.Try(
                async () =>
                {
                    // Check if the request contains multipart/form-data
                    if (!this.Request.Content.IsMimeMultipartContent("form-data"))
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                    var provider = await this.Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

                    //access files
                    IList<HttpContent> files = provider.Files;

                    //Example: reading a file's stream like below
                    HttpContent file = files[0];
                    Stream fileStream = await file.ReadAsStreamAsync();

                    await this._fileServerRepository.ReplaceEpubFile(id, fileStream);
                });
        }

        /// <summary>
        /// Deletes the epub.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="DeleteEpub">OK</response>
        /// <returns></returns>
        [HttpDelete]
        [Auth]
        [Route("{id}/epub")]
        public async Task<IHttpActionResult> DeleteEpub(int id)
        {
            return await this.Try(
                async () =>
                {
                    await this._fileServerRepository.DeleteEpubFile(id);
                });
        }

        #endregion

        #region Cover

        /// <summary>
        /// Uploads the cover.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="UploadCover">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        [Route("{id}/cover")]
        public async Task<IHttpActionResult> UploadCover(int id)
        {
            return await this.Try(
                async () =>
                {
                    // Check if the request contains multipart/form-data
                    if (!this.Request.Content.IsMimeMultipartContent("form-data"))
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                    var provider = await this.Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

                    //access files
                    IList<HttpContent> files = provider.Files;

                    //Example: reading a file's stream like below
                    HttpContent file = files[0];
                    Stream fileStream = await file.ReadAsStreamAsync();

                    await this._fileServerRepository.SaveCoverFile(id, fileStream);
                });
        }

        /// <summary>
        /// Downloads the cover.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="DownloadCover">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/cover")]
        public async Task<IHttpActionResult> DownloadCover(int id)
        {

            return await this.TryRaw(
                async () =>
                {
                    var file = await this._fileServerRepository.GetCoverFile(id);

                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content =
                                    new ByteArrayContent(file.ToArray())
                                    {
                                        Headers = { ContentType = new MediaTypeHeaderValue("image/png") }
                                    }
                    };
                    return this.ResponseMessage(response);
                });

        }

        /// <summary>
        /// Replaces the cover.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="ReplaceCover">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPut]
        [Auth]
        [Route("{id}/cover")]
        public async Task<IHttpActionResult> ReplaceCover(int id)
        {
            return await this.Try(
                async () =>
                {
                    // Check if the request contains multipart/form-data
                    if (!this.Request.Content.IsMimeMultipartContent("form-data"))
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                    var provider = await this.Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

                    //access files
                    IList<HttpContent> files = provider.Files;

                    //Example: reading a file's stream like below
                    HttpContent file = files[0];
                    Stream fileStream = await file.ReadAsStreamAsync();

                    await this._fileServerRepository.ReplaceCoverFile(id, fileStream);
                });
        }

        /// <summary>
        /// Deletes the cover.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="DeleteCover">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpDelete]
        [Auth]
        [Route("{id}/cover")]
        public async Task<IHttpActionResult> DeleteCover(int id)
        {
            return await this.Try(
                async () =>
                {
                    await this._fileServerRepository.DeleteCoverFile(id);
                });
        }

        #endregion
    }
}
