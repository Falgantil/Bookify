﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.API.Attributes;
using Bookify.API.Provider;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("Files")]
    public class FilesController : BaseApiController
    {
        private readonly IFileServerRepository _fileServerRepository;

        public FilesController(IFileServerRepository fileServerRepository)
        {
            this._fileServerRepository = fileServerRepository;
        }

        #region Epub

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

        [HttpGet]
        [Auth]
        [Route("{id}/epub")]
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
