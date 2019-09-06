﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.Basic.Domain.SeedWork
{
    public class Tree
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
                Fingerprint = Guid.NewGuid().ToString();
            else
                Fingerprint = fingerprint;
        }
    }
}