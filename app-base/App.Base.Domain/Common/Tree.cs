﻿namespace App.Base.Domain.Common
{
    public class Tree : Entity
    {
        public int LValue { get; protected set; }
        public int RValue { get; protected set; }
        public string ParentId { get; protected set; }
        public string NodeType { get; protected set; }
        public int DisplayIndex { get; protected set; }
        /// <summary>
        /// 树标识
        /// </summary>
        public string Fingerprint { get; protected set; }

        public void SetParentId(string parentId)
        {
            ParentId = parentId;
        }

        public void SetLValue(int lValue)
        {
            LValue = lValue;
        }

        public void SetRValue(int rValue)
        {
            RValue = rValue;
        }

        public void SetFingerprint(string fingerprint = null)
        {
            if (string.IsNullOrWhiteSpace(fingerprint))
                Fingerprint = GuidGen.NewGUID();
            else
                Fingerprint = fingerprint;
        }
    }
}
