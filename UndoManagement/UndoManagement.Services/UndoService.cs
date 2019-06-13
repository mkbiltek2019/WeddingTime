using AIT.DomainUtilities;
using AIT.UndoManagement.Infrastructure.DbContext;
using AIT.UndoManagement.Infrastructure.Repositories;
using AIT.UndoManagement.Infrastructure.Serialization;
using AIT.UndoManagement.Infrastructure.UndoInterface;
using AIT.UndoManagement.Model.DTO;
using System;
using System.IO;
using System.Xml.Serialization;
using AIT.UtilitiesComponents.Extensions;

namespace AIT.UndoManagement.Services
{
    public class UndoService : IUndoService
    {
        private readonly IUndoRepository _undoRepository;
        private readonly IXmlSerializationTypeRegistry _typeRegistry;
        private readonly IUnitOfWork<UndoContext> _unitOfWork;

        public UndoService(IUndoRepository undoRepository, IUnitOfWork<UndoContext> unitOfWork, IXmlSerializationTypeRegistry typeRegistry)
        {
            _undoRepository = undoRepository;
            _unitOfWork = unitOfWork;
            _typeRegistry = typeRegistry;
        }

        public void Delete(string userId, Guid uniqueKey)
        {
            Undo undo = GetUndo(userId, uniqueKey);

            _undoRepository.Delete(undo);
            _unitOfWork.Save();
        }

        public object Undo(string userId, Guid uniqueKey)
        {
            Undo undo = GetUndo(userId, uniqueKey);

            return IsUndoValid(undo).IfTrueOrFalse(() =>
            {
                var undoCommand = Deserialize(undo);
                return undoCommand.Execute();
            },
            () =>
            {
                return null;                                // not valid undo item will be deleted as the last step of undo controller action
            });
        }

        public Guid RegisterUndoCommand(string userId, IUndoCommand undoCommand)
        {
            var serializedData = Serialize(undoCommand);
            var uniqueKey = Guid.NewGuid();

            var undo = new Undo
            {
                UserId = userId,
                UniqueKey = uniqueKey,
                TypeKey = _typeRegistry.GetTypeKeyFromSerializationAttribute(undoCommand.GetType()),
                SerializedData = serializedData,
                CreateDate = DateTime.UtcNow
            };

            _undoRepository.Insert(undo);
            _unitOfWork.Save();

            return uniqueKey;
        }

        private string Serialize(IUndoCommand undoCommand)
        {
            var stream = new MemoryStream();
            var serializer = new XmlSerializer(undoCommand.GetType());
            serializer.Serialize(stream, undoCommand);
            return Convert.ToBase64String(stream.ToArray());
        }

        private Undo GetUndo(string userId, Guid uniqueKey)
        {
            return _undoRepository.Find(userId, uniqueKey);
        }

        private IUndoCommand Deserialize(Undo undo)
        {            
            var typeToDeserialize = _typeRegistry.Lookup(undo.TypeKey);
            if (typeToDeserialize == null)
                throw new ArgumentException("Cannot deserialize data because the typeKey is not registered");

            var serializer = new XmlSerializer(typeToDeserialize);
            var stream = new MemoryStream(Convert.FromBase64String(undo.SerializedData));

            return serializer.Deserialize(stream) as IUndoCommand;
        }

        private bool IsUndoValid(Undo undo)
        {
            return DateTime.Now.Subtract(undo.CreateDate.ToLocalTime()).Seconds < 60;       // since undo panel is active for 30 seconds, if if undo item was created one minute ago don't allow to perform undo operation
        }
    }
}
