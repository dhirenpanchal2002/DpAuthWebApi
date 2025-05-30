﻿using MongoDB.Bson;

namespace DpAuthWebApi.Repository
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
